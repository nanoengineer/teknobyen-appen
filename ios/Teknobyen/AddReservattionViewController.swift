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
    
    var dateFrom: NSDate!
    var dateTo: NSDate!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        datePicker.datePickerMode = .DateAndTime
        datePicker.minimumDate = NSDate() // NSDate() automatically gives today's date
        
        let sixDays = NSTimeInterval(6.0 * 24.0 * 3600.0)
        datePicker.maximumDate = NSDate().dateByAddingTimeInterval(sixDays)
        
        datePicker.minuteInterval = 15
        datePicker.locale = NSLocale(localeIdentifier: "no_NO")

        commentField.placeholder = "Beskrivelse"
        
        durationPicker.hidden = true
        durationPicker.dataSource = self
        durationPicker.delegate = self
        
        dateFrom = datePicker.date
        updateLabels()

    }
  
    
    @IBOutlet weak var durationPicker: UIPickerView!
    
    @IBAction func userPressedOk(sender: AnyObject) {
     
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
        
        dateFormatter.dateFormat = "HH:mm"
        let startHour = dateFormatter.stringFromDate(dateFrom)
        
        let secondInterval = durationInSeconds()
        dateTo = dateFrom.dateByAddingTimeInterval(secondInterval)
        
        let stopHour = dateFormatter.stringFromDate(dateTo)
        let roomNumber = 418
        
        let reservation = Reservation(date: day, startHour: startHour, stopHour: stopHour, roomNumber: roomNumber, comment: comment)
        saveReservationToServer(reservation)
        self.delegate.reservationReceived(reservation)
        self.navigationController?.popViewControllerAnimated(true)
    }
    
    private func durationInSeconds() -> Double {
        return Double(currentHours * 3600 + currentMinutes * 60)
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
    
    var pickerData = [["0 timer","1 time", "2 timer", "3 timer"], ["0 minutter", "15 minutter", "30 minutter", "45 minutter"]]
    
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
