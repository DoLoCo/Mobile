﻿using System;
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
            get { return "map49.png"; }
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
			get { return "Settings"; }
		}

		public override string Icon
		{
			get { return "tools3.png"; }
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
