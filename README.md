# teknobyen-appen
The Teknobyen App for Android/iOS and Windows PC/Mobile

#### Table of Contents
* [Introduction](#introduction)
* [V1.0 Features](#v1.0-features)
* [Feature Specifications](#feature-specifications)
* [Contributors](#contributors)

## Introduction
The Teknobyen App is a cross-platform mobile application developed for the residents of Teknobyen to conveniently access useful information ... (WIP)

## V1.0 Features

The following are the features that will be implemented in the first public release.

* __Login Page__
  * Ask the user to login with their washing machine credentials, name, and room number.


* __Book Projector__
  * Add reservations
  * Edit and remove own reservations
  * Disallow conflicting times when adding reservation


* __Washing Machine Status__
  * Check real-time washing machine status
  * Show wallet amount


* __Cleaning List__
  * @SindreSB fill this in!

## Feature Specifications
* __Login Page__

    * ##### Required fields:
      * __Name__
      * __Room number__
      * __washing machine ID__
      * __washing machine Password__ (make sure the text field is in secure mode).

    * When the user fills in their washing machine credentials, check the validity with a background HTTP request to the washing machine website. If not authorized, then do not let the user log in. Up to the specific developer on how the UI is implemented for this.

    * The washing machine credentials should be stored in securely with an API provided by the OS platform (e.g. on iOS it's the iOS Keychain)


* __Book Projector__

    * ###### Firebase Data Format: (All fields are strings)
      * __userID__: AAAA BAAA AAAG AA
      * __comment__: Game of Thrones
      * __name__ : Sindre
      * __roomNumber__ : 503
      * __date__ : dd.mm.yyyy (09.05.2016)
      * __startTime__ : hh.mm (19.00)
      * __duration__ : integer.decimal in units of hours (2.75)

    * ###### Maximum duration per reservation : x hours
    * ###### Maximum bookings per day per user : x

    * ###### Adding reservations:
      * Input userID, name, and roomNumber from saved Login credentials
      * Allow user to choose date, starting time, and duration. Also allow optional comments (140 chars max?) that describes the booking purpose. When choosing duration, only allow increments of 15 minutes

    * ###### Removing/Editing reservations
      * The user can edit or remove their own reservations. Identify the owner of the reservation with the __userID__ field.


* __Washing Machine Status__

    * ###### Status Display
      * Fetch the HTML from 'http://129.241.152.11/LaundryState?lg=2&ly=9131' by doing a basic HTTP request with the washing machine credentials provided by the user during login, and parse it with the method of your choice.

      If using XPath, this is the expression: `"//div[contains(@class, 'reservation')]/table//td[contains(@class, 'p')]/text()"`

      * States
        * LEDIG
        * XY MIN.
        * STATUS UNKNOWN

        (STATUS UNKNOWN should be shown if the HTTP request failed during INITIAL request. If statuses could not be retrieved in subsequent requests, then show the last fetched statuses and display somewhere that an network error occured).

      * State Colors
             ```swift
             let machineAvailColor = UIColor(rgba: "#97E37DD5")
             let machineBusyColor = UIColor(rgba: "#E3967DC5")
             let machineUnknownColor = UIColor(rgba: "#E3D059C5")
             ```

      * There should be a way for the user to fetch the latest status (e.g. button or pull to refresh) and there should also be a field somewhere that shows when the statuses were last updated. (e.g. Just Now, or 5 min. ago)

* __Cleaning List__

    @SindreSB Fill this in please :)

## Contributors

  * Android: [Alexolo](https://github.com/Alexolo), [SolveH](https://github.com/SolveH), [arnagl](https://github.com/arnagl)
  * iOS: [nanoengineer](https://github.com/nanoengineer), [Misolini](https://github.com/Misolini)
  * Windows PC/Mobile: [SindreSB](https://github.com/SindreSB)
