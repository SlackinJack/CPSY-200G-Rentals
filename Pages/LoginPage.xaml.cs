namespace Rentals.Pages {

	public partial class LoginPage : ContentPage {
		public LoginPage() {
			InitializeComponent();
		}

		private async void onLoginButtonClicked(System.Object sender, System.EventArgs e) {
			if (this.usernameEntry.Text == "username" && this.passwordEntry.Text == "password") {
				await Navigation.PushAsync(new MainPage());
            } else {
				await this.DisplayAlert("Error", "Wrong Credentials.\n(username: username, password: password", "oops");
			}
		}
	}
}