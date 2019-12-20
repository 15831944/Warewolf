
using System;
using System.IO;
using System.Reflection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExpectedExceptionAttribute : NUnitAttribute, IWrapTestMethod
    {
        private readonly Type _expectedExceptionType;
        private readonly string _expectedMessage;

        public ExpectedExceptionAttribute(Type type)
        {
            _expectedExceptionType = type;
        }

        public ExpectedExceptionAttribute(Type type, string message)
        {
            _expectedExceptionType = type;
            _expectedMessage = message;
        }

        public TestCommand Wrap(TestCommand command)
        {
            return new ExpectedExceptionCommand(command, _expectedExceptionType, _expectedMessage);
        }

        private class ExpectedExceptionCommand : DelegatingTestCommand
        {
            private readonly Type _expectedType;
            private readonly string _expectedMessage;

            public ExpectedExceptionCommand(TestCommand innerCommand, Type expectedType, string expectedMessage)
                : base(innerCommand)
            {
                _expectedType = expectedType;
                _expectedMessage = expectedMessage;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                Type caughtType = null;
                string caughtMessage = null;
                try
                {
                    innerCommand.Execute(context);
                }
                catch (Exception ex)
                {
                    if (ex is NUnitException)
                        ex = ex.InnerException;
                    caughtType = ex.GetType();
                    caughtMessage = ex.Message;
                }

                if (!string.IsNullOrEmpty(_expectedMessage))
                {
                    Assert.IsTrue(caughtMessage == _expectedMessage);
                }

                if (caughtType == _expectedType)
                    context.CurrentResult.SetResult(ResultState.Success);
                else if (caughtType != null)
                    context.CurrentResult.SetResult(ResultState.Failure,
                        string.Format("Expected {0} but got {1}", _expectedType.Name, caughtType.Name));
                else
                    context.CurrentResult.SetResult(ResultState.Failure,
                        string.Format("Expected {0} but no exception was thrown", _expectedType.Name));

                return context.CurrentResult;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true,
    Inherited = false)]
    public class DeploymentItem : System.Attribute
    {
        private readonly string _itemPath;
        private readonly string _filePath;
        private readonly string _binFolderPath;
        private readonly string _itemPathInBin;
        private readonly DirectoryInfo _environmentDir;
        private readonly Uri _itemPathUri;
        private readonly Uri _itemPathInBinUri;

        public DeploymentItem(string fileProjectRelativePath)
        {
            _filePath = fileProjectRelativePath.Replace("/", @"\");

            _environmentDir = new DirectoryInfo(Environment.CurrentDirectory);
            _itemPathUri = new Uri(Path.Combine(_environmentDir.Parent.Parent.FullName
                , _filePath));

            _itemPath = _itemPathUri.LocalPath;
            _binFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _itemPathInBinUri = new Uri(Path.Combine(_binFolderPath, _filePath));
            _itemPathInBin = _itemPathInBinUri.LocalPath;

            if (File.Exists(_itemPathInBin))
            {
                File.Delete(_itemPathInBin);
            }

            if (File.Exists(_itemPath))
            {
                File.Copy(_itemPath, _itemPathInBin);
            }
        }

        public DeploymentItem(string fileProjectRelativePath, string outputFolderRelativePath)
        {
            _filePath = fileProjectRelativePath.Replace("/", @"\");

            _environmentDir = new DirectoryInfo(Environment.CurrentDirectory);
            _itemPathUri = new Uri(Path.Combine(_environmentDir.Parent.Parent.FullName, _filePath));

            _itemPath = _itemPathUri.LocalPath;
            _binFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _itemPathInBinUri = new Uri(Path.Combine(_binFolderPath, outputFolderRelativePath, _filePath));
            _itemPathInBin = _itemPathInBinUri.LocalPath;

            if (File.Exists(_itemPathInBin))
            {
                File.Delete(_itemPathInBin);
            }

            if (File.Exists(_itemPath))
            {
                File.Copy(_itemPath, _itemPathInBin);
            }
        }
    }
}