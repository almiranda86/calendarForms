using Xamarin.Forms;
using System;

namespace calendar
{
	public partial class calendarPage : ContentPage
	{
		//create a grid to print the calendar
		Grid gridCalendar = new Grid();
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
				label.FontSize = 10;

				switch (d)
				{
					case 0:
						label.Text = "Sunday";

						gridCalendar.Children.Add(label, d, 0);
						break;
					case 1:
						label.Text = "Monday";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 2:
						label.Text = "Tuesday";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 3:
						label.Text = "Wednesday";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 4:
						label.Text = "Thursday";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 5:
						label.Text = "Friday";
						gridCalendar.Children.Add(label, d, 0);
						break;

					case 6:
						label.Text = "Saturday";
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
				var x = new DateTime(date.Year, date.Month, i);

				switch (x.DayOfWeek)
				{
					case DayOfWeek.Sunday:
						week++;
						writeCalendarRow(week, 0, i);
						break;
					case DayOfWeek.Monday:
						writeCalendarRow(week, 1, i);
						break;
					case DayOfWeek.Tuesday:
						writeCalendarRow(week, 2, i);
						break;
					case DayOfWeek.Wednesday:
						writeCalendarRow(week, 3, i);
						break;
					case DayOfWeek.Thursday:
						writeCalendarRow(week, 4, i);
						break;
					case DayOfWeek.Friday:
						writeCalendarRow(week, 5, i);
						break;
					case DayOfWeek.Saturday:
						writeCalendarRow(week, 6, i);
						break;
				}
			}

			calendarBody.Children.Add(gridCalendar);
		}

		private void writeCalendarRow(int week, int weekday, int day)
		{
			//we build the description
			Label description = new Label();
			description.Text = day.ToString();
			description.FontSize = 14;
			description.HorizontalOptions = LayoutOptions.Center;
			description.VerticalOptions = LayoutOptions.Center;
			description.TextColor = Color.Black;

			//and we build the button
			StackLayout calendarDay = new StackLayout();
			calendarDay.Children.Add(description);
			calendarDay.Padding = 5;
			calendarDay.BackgroundColor = Color.White;

			calendarDay.GestureRecognizers.Add(new TapGestureRecognizer()
			{
				Command = new Command(() =>
				{
					//Todo: change background color, of clicked item, and if possible, the range
				})
			});

			gridCalendar.Children.Add(calendarDay, weekday, week);
		}
	}
}
