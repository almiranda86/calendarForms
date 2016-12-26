using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace calendar
{
	public partial class listPage : ContentPage
	{
		public listPage(object chosenDate)
		{
			InitializeComponent();

			ListView listDate = new ListView();
			listDate.ItemsSource = (List<DateTime>)chosenDate;

			listDateContainer.Children.Add(listDate);
		}
	}
}
