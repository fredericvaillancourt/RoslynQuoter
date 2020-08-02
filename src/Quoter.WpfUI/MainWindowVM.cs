using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RoslynQuoter;

namespace Quoter.WpfUI
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private bool _openParenthesisOnNewLine;
        private bool _closingParenthesisOnNewLine;
        private bool _useDefaultFormatting = true;
        private bool _removeRedundantModifyingCalls = true;
        private bool _shortenCodeWithUsingStatic = true;
        private string _inputText;
        private string _outputText;
        private NodeKind _nodeKind;

        public MainWindowVM()
        {
            GenerateCommand = new RelayCommand(Generate);
        }

        public NodeKind[] NodeKinds { get; } = (NodeKind[])Enum.GetValues(typeof(NodeKind));

        public bool OpenParenthesisOnNewLine
        {
            get { return _openParenthesisOnNewLine; }
            set
            {
                if (_openParenthesisOnNewLine != value)
                {
                    _openParenthesisOnNewLine = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ClosingParenthesisOnNewLine
        {
            get { return _closingParenthesisOnNewLine; }
            set
            {
                if (_closingParenthesisOnNewLine != value)
                {
                    _closingParenthesisOnNewLine = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool UseDefaultFormatting
        {
            get { return _useDefaultFormatting; }
            set
            {
                if (_useDefaultFormatting != value)
                {
                    _useDefaultFormatting = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveRedundantModifyingCalls
        {
            get { return _removeRedundantModifyingCalls; }
            set
            {
                if (_removeRedundantModifyingCalls != value)
                {
                    _removeRedundantModifyingCalls = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShortenCodeWithUsingStatic
        {
            get { return _shortenCodeWithUsingStatic; }
            set
            {
                if (_shortenCodeWithUsingStatic != value)
                {
                    _shortenCodeWithUsingStatic = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GenerateCommand { get; }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OutputText
        {
            get { return _outputText; }
            set
            {
                if (_outputText != value)
                {
                    _outputText = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public NodeKind NodeKind
        {
            get { return _nodeKind; }
            set
            {
                if (_nodeKind != value)
                {
                    _nodeKind = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Generate()
        {
            var quoter = new RoslynQuoter.Quoter
            {
                OpenParenthesisOnNewLine = OpenParenthesisOnNewLine,
                ClosingParenthesisOnNewLine = ClosingParenthesisOnNewLine,
                UseDefaultFormatting = UseDefaultFormatting,
                ShortenCodeWithUsingStatic = ShortenCodeWithUsingStatic,
                RemoveRedundantModifyingCalls = RemoveRedundantModifyingCalls
            };

            var generatedNode = quoter.Quote(_inputText, NodeKind);
            OutputText = quoter.Print(generatedNode);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
