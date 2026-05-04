namespace F1_Car_Garage.Models // The ErrorViewModel class displays an error page when the request fails, and it contains a RequestId property to track the request that caused the error, and a ShowRequestId property to determine whether to display the RequestId on the error page. It is used in the HomeController's Error action to pass the error information to the view.
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
