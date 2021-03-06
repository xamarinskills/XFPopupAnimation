﻿using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFPopupAnimation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegPage : PopupPage
	{
		public RegPage ()
		{
			InitializeComponent ();
		}
        public bool IsAnimationEnabled { get; private set; } = true;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;
            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                LoginButton.Scale = 1;
                LoginButton.Opacity = 1;

                FnameEntry.TranslationX =
                LnameEntry.TranslationX =
                UsernameEntry.TranslationX =
                PasswordEntry.TranslationX = 0;
                FnameEntry.TranslationX =
                LnameEntry.TranslationX =
                UsernameEntry.Opacity =
                PasswordEntry.Opacity = 1;

                return;
            }

            CloseImage.Rotation = 380;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;
            LoginButton.Scale = 0.3;
            LoginButton.Opacity = 0;
            FnameEntry.TranslationX =
            LnameEntry.TranslationX =
            UsernameEntry.TranslationX =
            PasswordEntry.TranslationX = -10;
            FnameEntry.Opacity =
            LnameEntry.Opacity =
            UsernameEntry.Opacity =
            PasswordEntry.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEnd()
        {
            if (!IsAnimationEnabled)
                return;

            var translateLength = 800u;

            await Task.WhenAll(
                        FnameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        FnameEntry.FadeTo(1),
                //LnameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                //LnameEntry.FadeTo(1),

                //UsernameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                //UsernameEntry.FadeTo(1),

                (new Func<Task>(async () =>
                {
                    await Task.Delay(400);
                    await Task.WhenAll(

                 LnameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                 LnameEntry.FadeTo(1),
                 Task.Delay(200),
                UsernameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                UsernameEntry.FadeTo(1),
                Task.Delay(200),
                PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        PasswordEntry.FadeTo(1));
                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                LoginButton.ScaleTo(1),
                LoginButton.FadeTo(1));
        }

        protected override async Task OnDisappearingAnimationBegin()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            await Task.WhenAll(
                FnameEntry.FadeTo(0),
                LnameEntry.FadeTo(0),
                UsernameEntry.FadeTo(0),
                PasswordEntry.FadeTo(0),
                LoginButton.FadeTo(0));

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(500);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
    }
}