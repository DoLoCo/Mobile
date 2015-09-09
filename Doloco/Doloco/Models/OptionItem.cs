using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Models
{
    public class HomeOptionItem : OptionItem
    {
        public override string Title
        {
            get { return "Nearby"; }
        }

        public override string Icon
        {
            get { return "about.png"; }
        }
    }

    public class CirclesOptionItem : OptionItem
    {
        public override string Title
        {
            get { return "Circles"; }
        }

        public override string Icon
        {
            get { return "map52.png"; }
        }
    }

	public class SettingsOptionItem : OptionItem {
		public override string Title
		{
			get { return "Payment Info"; }
		}

		public override string Icon
		{
			get { return "bank.png"; }
		}
	}

    public class PrivacyOptionItem : OptionItem
    {
        public override string Title
        {
            get { return "Privacy"; }
        }

        public override string Icon
        {
            get { return "privacy.png"; }
        }
    }

    public class TermsOptionItem : OptionItem
    {
        public override string Title
        {
            get { return "Terms"; }
        }

        public override string Icon
        {
            get { return "terms.png"; }
        }
    }

    public class FeedbackOptionItem : OptionItem
    {
        public override string Title
        {
            get { return "Send FeedBack"; }
        }

        public override string Icon
        {
            get { return "feedback.png"; }
        }
    }

    public class AboutOptionItem : OptionItem
    {
        public override string Title
        {
            get { return "About Us"; }
        }

        public override string Icon
        {
            get { return "about.png"; }
        }
    }

    public class OptionItem
    {
        public virtual string Title { get { var n = GetType().Name; return n.Substring(0, n.Length - 10); } }
        public virtual int Count { get; set; }
        public virtual bool Selected { get; set; }
        public virtual string Icon { get { return "item.png"; } }
        public ImageSource IconSource { get { return ImageSource.FromFile(Icon); } }
    }
}
