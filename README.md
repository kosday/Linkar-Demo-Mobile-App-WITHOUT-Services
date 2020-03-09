# Linkar Demo Mobile App WITHOUT Services

Calls are made using a DIRECT CONNECTION to the Linkar Server. The mobile device will connect directly with Linkar Server. No need to use a Web Service.


LinkarViewFiles (Shared Project)

LinkarViewFiles.Android.

LinkarViewFiles.IOS.

All 3 projects are created automatically when you choose Cross-Platform Mobile Application template (Xamarin.Forms). Within this template you select the platform where it is addressed (Android and IOS) and code sharing strategy (shared project). This demo uses the Master-Detail template as a base.

Login
Access data is not captured on the screen in this project, in this case we have established them internally. We only use this page to generate a token against the web service that validates us for the rest of the calls

About Demo
Summary page about the functionality of the rest of pages.

Customers
This list brings all the Customers from the database, the detail shows each one when we click on each litem

Items
This list brings all the Items from the database, the detail shows each one of them when we click on each item.

Orders
This list brings all the Orders from the database, the detail shows each one when we click on the orden. We can navigate through the multivalues and subvalues selecting them.
