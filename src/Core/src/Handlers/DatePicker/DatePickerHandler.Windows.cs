﻿using System;
using Microsoft.UI.Xaml.Controls;

namespace Microsoft.Maui.Handlers
{
	public partial class DatePickerHandler : AbstractViewHandler<IDatePicker, DatePicker>
	{
		protected override DatePicker CreateNativeView() => new DatePicker();

		public static void MapFormat(DatePickerHandler handler, IDatePicker datePicker) { }
		public static void MapDate(DatePickerHandler handler, IDatePicker datePicker) { }
		public static void MapMinimumDate(DatePickerHandler handler, IDatePicker datePicker) { }
		public static void MapMaximumDate(DatePickerHandler handler, IDatePicker datePicker) { }
		public static void MapFont(DatePickerHandler handler, IDatePicker datePicker) { }
	}
}