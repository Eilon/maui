﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	public partial class DatePickerHandlerTests
	{
		[Fact(DisplayName = "Minimum Date Initializes Correctly")]
		public async Task MinimumDateInitializesCorrectly()
		{
			DateTime xplatMinimumDate = new DateTime(2000, 01, 01);

			var datePicker = new DatePickerStub()
			{
				MinimumDate = xplatMinimumDate,
				Date = DateTime.Today
			};

			DateTime expectedValue = xplatMinimumDate;

			var values = await GetValueAsync(datePicker, (handler) =>
			{
				return new
				{
					ViewValue = datePicker.MinimumDate,
					NativeViewValue = GetNativeMinimumDate(handler)
				};
			});

			Assert.Equal(xplatMinimumDate, values.ViewValue);
			Assert.Equal(expectedValue, values.NativeViewValue);
		}

		[Fact(DisplayName = "Maximum Date Initializes Correctly")]
		public async Task MaximumDateInitializesCorrectly()
		{
			DateTime xplatMaximumDate = new DateTime(2030, 01, 01);

			var datePicker = new DatePickerStub()
			{
				MinimumDate = new DateTime(2000, 01, 01),
				MaximumDate = new DateTime(2030, 01, 01),
				Date = DateTime.Today
			};

			DateTime expectedValue = xplatMaximumDate;

			var values = await GetValueAsync(datePicker, (handler) =>
			{
				return new
				{
					ViewValue = datePicker.MaximumDate,
					NativeViewValue = GetNativeMaximumDate(handler)
				};
			});

			Assert.Equal(xplatMaximumDate, values.ViewValue);
			Assert.Equal(expectedValue, values.NativeViewValue);
		}

		[Theory(DisplayName = "Font Family Initializes Correctly")]
		[InlineData(null)]
		[InlineData("Times New Roman")]
		[InlineData("Dokdo")]
		public async Task FontFamilyInitializesCorrectly(string family)
		{
			var datePicker = new DatePickerStub()
			{
				Date = DateTime.Today,
				Font = Font.OfSize(family, 10)
			};

			var handler = await CreateHandlerAsync(datePicker);
			var nativeFont = await GetValueAsync(datePicker, handler => GetNativeDatePicker(handler).Font);

			var fontManager = handler.Services.GetRequiredService<IFontManager>();

			var expectedNativeFont = fontManager.GetFont(Font.OfSize(family, 0.0));

			Assert.Equal(expectedNativeFont.FamilyName, nativeFont.FamilyName);
			if (string.IsNullOrEmpty(family))
				Assert.Equal(fontManager.DefaultFont.FamilyName, nativeFont.FamilyName);
			else
				Assert.NotEqual(fontManager.DefaultFont.FamilyName, nativeFont.FamilyName);
		}

		MauiDatePicker GetNativeDatePicker(DatePickerHandler datePickerHandler) =>
			(MauiDatePicker)datePickerHandler.View;

		DateTime GetNativeDate(DatePickerHandler datePickerHandler)
		{
			var dateString = GetNativeDatePicker(datePickerHandler).Text;
			DateTime.TryParse(dateString, out DateTime result);

			return result;
		}

		DateTime GetNativeMinimumDate(DatePickerHandler datePickerHandler)
		{
			var dialog = datePickerHandler.DatePickerDialog;
			var minDate = dialog.MinimumDate;

			return minDate.ToDateTime();
		}

		DateTime GetNativeMaximumDate(DatePickerHandler datePickerHandler)
		{
			var dialog = datePickerHandler.DatePickerDialog;
			var maxDate = dialog.MaximumDate;

			return maxDate.ToDateTime();
		}

		double GetNativeUnscaledFontSize(DatePickerHandler datePickerHandler) =>
			GetNativeDatePicker(datePickerHandler).Font.PointSize;
	}
}