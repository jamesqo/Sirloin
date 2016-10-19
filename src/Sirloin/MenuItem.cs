using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Sirloin
{
    public sealed class MenuItem
    {
        public string Label { get; set; }
        private Type destinationPageType;
        public Type DestinationPageType
        {
            get { return destinationPageType; }
        }
        private string destinationPage;
        public string DestinationPage
        {
            get { return destinationPage; }
            set
            {
                destinationPage = value;
                destinationPageType = Type.GetType(destinationPage);
            }
        }
        public object Argument { get; set; }
        public string SymbolText { get; set; }

        public Symbol Symbol
        {
            get
            {
                var text = this.SymbolText;
                if (string.IsNullOrEmpty(text))
                    return default(Symbol);

                char first = text[0];
                return (Symbol)first;
            }
            set
            {
                char c = (char)value;
                this.SymbolText = c.ToString();
            }
        }
    }
}