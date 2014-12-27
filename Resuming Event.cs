// This must be coded in App.cs

// From the Suspended state, your app can assume the NotRunning and Running states.
// You don’t have control over the transition to the NotRunning state, but you can control the behavior when passed 
// to the Running state by firing the Resuming event.
// C# Managing the Resuming event is really easy. The event handler can be implemented as follows in App.xaml.cs: 

void OnResuming(object sender, object e) 
{
     //Place your code here 
} 

//Attach the event handler to the Resuming event in the App() constructor: 

this.Resuming += OnResuming; 