using System;
using System.Windows.Forms;

namespace QuickWaifu2x.Soju06.WinForms {
    public class UInt32InputBox : TextBox {
        public UInt32InputBox() {

        }

        protected override void OnTextChanged(EventArgs e) {
            if (Text.All(r => char.IsDigit(r))) {
                Text = Text.Filter(e => char.IsDigit(e)).Concat();
            }
            if (uint.TryParse(Text, out var o))
                number = o;
            base.OnTextChanged(e);
        }

        uint number = 0;

        public uint Number { get => number; set => Text = value.ToString(); }

        protected override void OnKeyPress(KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            base.OnKeyPress(e);
        }
    }
}
