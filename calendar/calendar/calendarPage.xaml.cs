using Xamarin.Forms;
using System;
using System.Collections.Generic;

namespace calendar
{
	public partial class calendarPage : ContentPage
	{
		bool isGridEnable = true;
		List<DateTime> dateChosen = new List<DateTime>();
		List<DateTime> dateRange = new List<DateTime>();

		//create a grid to print the calendar
		Grid gridCalendar;
		DateTime date = new DateTime();

		public calendarPage()
		{
			InitializeComponent();

			//from the current day, we can read all the month and build the calendar
			date = DateTime.Now;

			//we call the function that will write the calendar itself
			writeCalendar(date);
		}

		private void writeCalendar(DateTime _date)
		{
			gridCalendar = new Grid();
			gridCalendar.BackgroundColor = Color.FromHex("#e5e6eb");

			//we allways will have 7 columns in a calendar
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			//the first row indicates the days of week
			gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


			for (int d = 0; d < 7; d++)
			{
				Label label = new Label();
				label.FontSize = 16;
				label.HorizontalTextAlignment = TextAlignment.Center;
				label.VerticalTextAlignment = TextAlignment.Center;

				switch (d)
				{
					case 0:
						label.Text = "Sun";
						gridCalendar.Children.Add(label, d, 0);
						break;
					case 1:
						label.Text = "Mon";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 2:
						label.Text = "Tue";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 3:
						label.Text = "Wed";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 4:
						label.Text = "Thu";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 5:
						label.Text = "Fri";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 6:
						label.Text = "Sat";
						gridCalendar.Children.Add(label, d, 0);
						break;

				}
			}


			//get the days in the current month
			var days = DateTime.DaysInMonth(_date.Year, _date.Month);

			//found the number of weeks in the current month
			int rows = days / 7;

			//write the number of rows of the current month
			for (int y = 0; y < rows; y++)
			{
				gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			}

			int week = 1;
			//for each days founs, we discover the respective week day
			for (int i = 1; i <= days; i++)
			{
				var x = new DateTime(_date.Year, _date.Month, i);

				switch (x.DayOfWeek)
				{
					case DayOfWeek.Sunday:
						week++;
						writeCalendarRow(week, 0, i, x);
						break;
					case DayOfWeek.Monday:
						writeCalendarRow(week, 1, i, x);
						break;
					case DayOfWeek.Tuesday:
						writeCalendarRow(week, 2, i, x);
						break;
					case DayOfWeek.Wednesday:
						writeCalendarRow(week, 3, i, x);
						break;
					case DayOfWeek.Thursday:
						writeCalendarRow(week, 4, i, x);
						break;
					case DayOfWeek.Friday:
						writeCalendarRow(week, 5, i, x);
						break;
					case DayOfWeek.Saturday:
						writeCalendarRow(week, 6, i, x);
						break;
				}
			}

			switch (_date.Month)
			{
				case 1:
					monthName.Text = "January";
					break;
				case 2:
					monthName.Text = "February";
					break;
				case 3:
					monthName.Text = "March";;
					break;
				case 4:
					monthName.Text = "April";;
					break;
				case 5:
					monthName.Text = "May";;
					break;
				case 6:
					monthName.Text = "June";;
					break;
				case 7:
					monthName.Text = "July";;
					break;
				case 8:
					monthName.Text = "August";
					break;
				case 9:
					monthName.Text = "September";
					break;
				case 10:
					monthName.Text = "October";
					break;
				case 11:
					monthName.Text = "November";
					break;
				case 12:
					monthName.Text = "December";
					break;					
			}



			if (calendarBody.Children.Count > 0)
			{
				calendarBody.Children[0] = gridCalendar;
			}
			else {
				calendarBody.Children.Add(gridCalendar);
			}
		}

		private void writeCalendarRow(int week, int weekday, int day, DateTime dateTime)
		{
			//we build the description
			Label description = new Label();
			description.Text = day.ToString();
			description.FontSize = 14;
			description.HorizontalOptions = LayoutOptions.Center;
			description.VerticalOptions = LayoutOptions.Center;
			description.TextColor = Color.Black;

			Label _date = new Label();
			_date.Text = dateTime.Date.ToString();
			_date.IsVisible = false;

			//and we build the button
			StackLayout calendarDay = new StackLayout();
			calendarDay.Children.Add(description);

			//a way out to bring along the date time information
			calendarDay.Children.Add(_date);

			calendarDay.Padding = 5;
			calendarDay.BackgroundColor = Color.White;
			calendarDay.IsEnabled = isGridEnable;

			//define the event on the click of a day
			var tapCalendarDay = new TapGestureRecognizer();
			tapCalendarDay.Tapped += (sender, e) => OnSelectCalendarDay(sender, e);

			calendarDay.GestureRecognizers.Add(tapCalendarDay);

			gridCalendar.Children.Add(calendarDay, weekday, week);
		}

		//this function looks more like a work around than anything else...
		void OnSelectCalendarDay(object sender, EventArgs args) {

			//first...
			//cast the sender to the original type
			var s = sender as StackLayout;

			//then..
			//retrieve the date time info
			var x = s.Children[1];

			//after...
			//cast it to the original object
			Label l = (Label)x;


			//and finally...
			//and retrieve the info we want
			DateTime d = DateTime.Parse(l.Text);

			//a huge way to achieve a small piece of information...


			//now, we look into the list if this date time is already there...
			if (dateRange.Contains(d))
			{
				//if yes, we remove and change the calendar color back to white...
				dateRange.Sort();

				//retrieve the parent of the sender(StackLayout)
				Grid g = (Grid)s.Parent;
				//g.IsEnabled = false;

				//verify how many children the parent has...
				int children = g.Children.Count;

				//start the loop at the position 7, cause the seven first ones are the weekdays descriptions
				for (int i = 7; i < children; i++)
				{
					//do the same work to retrieve the Date info
					StackLayout _s = (StackLayout)g.Children[i];
					Label _l = (Label)_s.Children[1];
					DateTime _d = DateTime.Parse(_l.Text);

					//if the Date checked is the same informed, or higher, we remove then from the range
					if (_d >= d) { 
						_s.BackgroundColor = Color.White;

						if (dateRange.Contains(_d)) { 
							dateRange.Remove(_d);
						}

						if (dateChosen.Contains(_d))
						{
							dateChosen.Remove(_d);
						}

						if (dateChosen.Count == 1) { break; }
					}
				}
			}
			else { 
				//cause no, we add the value into the list, and color the calendar day with a light red
				dateChosen.Add(d);
				dateRange.Add(d);
				s.BackgroundColor = Color.FromHex("#ffbdbd");
			}


			//when we choose 2 dates on the calendar, we lock it, and indicate on the UI the range selected
			if (dateChosen.Count > 1)
			{
				//retrieve the parent of the sender(StackLayout)
				Grid g = (Grid)s.Parent;
				//g.IsEnabled = false;

				//verify how many children the parent has...
				int children = g.Children.Count;

				//start the loop at the position 7, cause the seven first ones are the weekdays descriptions
				for (int i = 7; i < children; i++)
				{
					//do the same work to retrieve the Date info
					StackLayout _s = (StackLayout)g.Children[i];
					Label _l = (Label)_s.Children[1];
					DateTime _d = DateTime.Parse(_l.Text);

					//verify if the Date found are in the list of choosen
					if (!dateRange.Contains(_d))
					{
						//cause no, we color the background with a light red and then insert it into the list
						_s.BackgroundColor = Color.FromHex("#ffbdbd");
						dateRange.Add(_d);
					}

					//when the Date tested are equal to the last of the two choose, we break the loop
					if (_d == dateChosen[1])
					{
						break;
					}
				}
			}
		}


		void OnPrevMonth(object sender, EventArgs args)
		{
			var prevDate = date.AddMonths(-1);
			date = prevDate;
			writeCalendar(prevDate);
		}

		void OnNextMonth(object sender, EventArgs args)
		{
			var nextDate = date.AddMonths(+1);
			date = nextDate;
			writeCalendar(nextDate);
		}

		void OnSelectDates(object sender, EventArgs args) {
			
			this.Navigation.PushAsync(new listPage(dateRange));
		}
	}
}
