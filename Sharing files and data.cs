// Uri association
Windows.System.Launcher.LaunchUriAsync(new Uri("jumpstart:NewSession?ID=aea6"));

// File association
Windows.System.Launcher.LaunchFileAsync(myStorageFile);

// Share contract
Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();

//--> Custom URI Associantions

// Launch other apps to complete tasks
// Launch another app and pass it data
// Play an album on Spotify
// Play a video in YouTube
// Launch device settings

// Link into core experiences
// Browser(http)
// Messaging
// Email(mailto:)

await Launcher.LaunchUriAsync(new Uri("fb://profile/1234"))

//--> File Type Associations

// Launch files in the right app
// Microsoft Office
// Adobe Reader

// Handle custom files in your app from
//Another app
//Browser
//Email
//Office Hub

var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata://Local/mydoc.pdf"));
await Launcher.LaunchFileAsync(file);


// This must be coded in app.cs

//--> Handling File Activation(Windows Runtime Apps)

protected override async void OnFileActivated(FileActivatedEventArgs args)
{
  // Handle file actication. The number of files received is args.Files.Size. First file is args.Files[0].Name 
  Frame rootframe= Window.Current.Content as Frame;

  ... // Standard Frame initialization code ...

  if(rootframe.Content==null)
  {
    if(!rootFrame.Navigate(typeof(BugQueryPage))){ throw new Exception("Failed to create initial page");}
  }
  
  var p= rootFrame.Content as BugQueryPage;

  // Pass the File Activation args to a property you've implemented on the target page
  p.FileEvent= args;

  Windows.Current.Activate();
} 

//--> Handiling Uri Activation(Windows Runtime Apps)

public partial class App
{
   ... 
  protected override void OnActivated(IActivatedEventArgs args)
  {
    if (args.Kind == ActivationKind.Protocol)
      {
        ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs;
        
        // TODO: Handle URI activation
        // The received URI is eventArgs.Uri.AbsoluteUri 
        ... 
      } 
   }
 ... 
} 

//--> Implementing share source

protected override void OnNavigatedTo(NavigationEventArgs e)
{
  navigationHelper.OnNavigatedTo(e);
  DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
}

protected override void OnNavigatedFrom(NavigationEventArgs e) 
{
  navigationHelper.OnNavigatedFrom(e);
  DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested; 
}

 private void AppBarButton_Click(object sender, RoutedEventArgs e) 
{
  DataTransferManager.ShowShareUI();
}
 
// ! Always remove your event handlers
// Always tear down your event handlers when you’re done with them

//--> Implementing share source vazhdim

// Handle DataRequested event and provide DataPackage
void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args) 
{
  var request = args.Request; 

  request.Data.Properties.Title = "Share example";
  //You MUST set a Title!
  request.Data.Properties.Description = "This demonstrates how to share text to another app";
  request.Data.SetText(TextToShare.Text.Trim()); 
}

// Title
// You must set a Title on the Data Package.
// If you do not, the Share operation silently fails (no exception).
// Description
// Not used by the Share UI on Windows Phone (used by the Windows Share UI), but is available to the Share target.