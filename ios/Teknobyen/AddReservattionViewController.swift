//
//  ReservatorViewController.swift
//  Teknobyen
//
//  Created by Mathias Breistein on 30.04.2016.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import Firebase


class AddReservattionViewController: UIViewController, UIPickerViewDelegate, UIPickerViewDataSource {

    
    @IBOutlet weak var commentField: UITextField!
    
    @IBOutlet weak var fromDate: UILabel!
    @IBOutlet weak var toDate: UILabel!
    
    @IBOutlet weak var datePicker: UIDatePicker!
    @IBOutlet weak var reservationButton: UIButton!
    @IBOutlet weak var dateDurationSelectionButton: UIButton!
    
    var dateFrom: NSDate!
    var dateTo: NSDate!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        reservationButton.layer.cornerRadius = 5
        reservationButton.backgroundColor = AppConstants.themeGreenColor
        
        dateDurationSelectionButton.layer.cornerRadius = 3
        dateDurationSelectionButton.backgroundColor = AppConstants.themeBlueColor
        dateDurationSelectionButton.tintColor = UIColor.whiteColor()
        
        
        dateDurationSelectionButton.setTitle(AppConstants.projectorBookingToggleText.duration, forState: UIControlState.Normal)
        
        
        datePicker.datePickerMode = .DateAndTime
        datePicker.minimumDate = NSDate() // NSDate() automatically gives today's date
        
        let sixDays = NSTimeInterval(6.0 * 24.0 * 3600.0)
        datePicker.maximumDate = NSDate().dateByAddingTimeInterval(sixDays)
        
        datePicker.minuteInterval = 15
        datePicker.locale = NSLocale(localeIdentifier: "no_NO")

        commentField.placeholder = "feks. Game of Thrones"
        
        durationPicker.hidden = true
        durationPicker.dataSource = self
        durationPicker.delegate = self
        
        let tap: UITapGestureRecognizer = UITapGestureRecognizer(target: self, action: #selector(self.dismissKeyboard))
        view.addGestureRecognizer(tap)
        
        dateFrom = datePicker.date
        updateLabels()

    }
    
    override func viewWillAppear(animated: Bool) {
    
    }
    
    func dismissKeyboard() {
        //Causes the view (or one of its embedded text fields) to resign the first responder status.
        view.endEditing(true)
    }
  
    
    @IBOutlet weak var durationPicker: UIPickerView!
    
    
    @IBAction func dateDurationButtonIsPressed(sender: UIButton) {
        
        if sender.titleLabel?.text == AppConstants.projectorBookingToggleText.duration {
            sender.setTitle(AppConstants.projectorBookingToggleText.date, forState: UIControlState.Normal)
        }
        else {
              sender.setTitle(AppConstants.projectorBookingToggleText.duration, forState: UIControlState.Normal)
        }
        
        datePicker.hidden = !datePicker.hidden
        durationPicker.hidden = !durationPicker.hidden
        updateLabels()
    }
    
    
    @IBAction func datePickerAction(sender: AnyObject) {
        updateLabels()
    }
    
    private func updateLabels() {
        let dateFormatter = NSDateFormatter()
        dateFormatter.dateStyle = .FullStyle
        dateFormatter.timeStyle = .ShortStyle
        dateFormatter.locale = NSLocale(localeIdentifier: "no_NO")
        
        dateFormatter.doesRelativeDateFormatting = true
        
        
        let strDate = dateFormatter.stringFromDate(datePicker.date)
        
        
        self.dateFrom = datePicker.date
        self.fromDate.text = "Fra: " + strDate
        updateToDate()
    }
    
    var delegate: ReservationDelegate!
    
    
    // saves the current reservation to the Firebase server
    @IBAction func reservationComplete(sender: UIButton) {
        let dateFormatter = NSDateFormatter()
        dateFormatter.dateStyle = .ShortStyle
        dateFormatter.locale = NSLocale(localeIdentifier: "no_NO")
        
        
        let comment = commentField.text != nil ? commentField.text! : ""
        let day = dateFormatter.stringFromDate(dateFrom)
        
        dateFormatter.dateFormat = "HH.mm"
        let startHour = dateFormatter.stringFromDate(dateFrom)
        
        let secondInterval = durationInSeconds()
        dateTo = dateFrom.dateByAddingTimeInterval(secondInterval)

        let reservation = Reservation(userID: UserTBCredentials.username!,
                                      comment: comment,
                                      name: UserTBCredentials.name!,
                                      roomNumber: UserTBCredentials.roomNumber!,
                                      date: day,
                                      startTime: startHour,
                                      duration: "\(durationInHours())")

        saveReservationToServer(reservation)
        self.delegate.reservationReceived(reservation)
        self.navigationController?.popViewControllerAnimated(true)
    }
    
    private func durationInSeconds() -> Double {
        return Double(currentHours * 3600 + currentMinutes * 60)
    }
    
    private func durationInHours() -> Double {
        return Double(currentHours + currentMinutes/60)
    }
    
    private func updateToDate() {
        
        let dateFormatter = NSDateFormatter()
        dateFormatter.dateStyle = .FullStyle
        dateFormatter.timeStyle = .ShortStyle
        dateFormatter.locale = NSLocale(localeIdentifier: "no_NO")
        
        dateFormatter.doesRelativeDateFormatting = true
        
        
        let strDate = dateFormatter.stringFromDate(dateFrom.dateByAddingTimeInterval(durationInSeconds()))
        
        toDate.text = "Til: " + strDate
    }
    
 
    // MARK: Delegate implementation
    
    // In seconds
    var currentHours = 0
    var currentMinutes = 0
    
    func pickerView(pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        if component == 0 {
            currentHours = row
        } else {
            currentMinutes = 15 * row
        }
        updateToDate()
    }
    
    var pickerData = [["0 t","1 t", "2 t", "3 t"], ["0 min", "15 min", "30 min", "45 min"]]
    
    // The number of columns of data
    func numberOfComponentsInPickerView(pickerView: UIPickerView) -> Int {
        return 2
    }
    
    // The number of rows of data
    func pickerView(pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return pickerData[component].count
    }
    
    // The data to return for the row and component (column) that's being passed in
    func pickerView(pickerView: UIPickerView, titleForRow row: Int, forComponent component: Int) -> String? {
        return pickerData[component][row]
    }
    
    
    // Runs asynchronously!!!
    private func saveReservationToServer(reservation: Reservation) {
        // Create a reference to a Firebase location
        let ref = Constants.RootReference.childByAppendingPath("reservations")
        // Write data to Firebase
        let idRef = ref.childByAutoId()
        idRef.setValue(reservation.format())
    }
    
 
    
    
    private struct Constants {
        static let RootReference = Firebase(url:"https://teknobyen.firebaseio.com")
    }
   
    

    

}
