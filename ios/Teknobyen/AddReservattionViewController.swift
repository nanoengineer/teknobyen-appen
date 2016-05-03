//
//  ReservatorViewController.swift
//  Teknobyen
//
//  Created by Mathias Breistein on 30.04.2016.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import Firebase


class AddReservattionViewController: UIViewController {

    var idNumber = 0
    
    @IBOutlet weak var commentField: UITextField!
    
    @IBOutlet weak var fromDate: UILabel!
    @IBOutlet weak var toDate: UILabel!
    @IBOutlet weak var datePicker: UIDatePicker!
    
    var dateFrom: NSDate!
    var dateTo: NSDate!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        fetchIdFromFireBase()
        
        datePicker.datePickerMode = .DateAndTime
        datePicker.minimumDate = NSDate() // NSDate() automatically gives today's date
        
        let sixDays = NSTimeInterval(6.0 * 24.0 * 3600.0)
        datePicker.maximumDate = NSDate().dateByAddingTimeInterval(sixDays)
        
        datePicker.minuteInterval = 15
        datePicker.locale = NSLocale(localeIdentifier: "no_NO")

        commentField.placeholder = "Beskrivelse"
        dateFrom = datePicker.date
        dateTo = datePicker.date
        updateLabels()

    }
    
    var selectedFrom = false
    
    @IBAction func userPressedOk(sender: AnyObject) {
        updateLabels()
        selectedFrom = !selectedFrom
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
        
        if selectedFrom {
            self.toDate.text = "Til: " + strDate
            self.dateTo = datePicker.date
        } else {
            self.dateFrom = datePicker.date
            self.fromDate.text = "Fra: " + strDate
            self.toDate.text = "Til: " + strDate
        }
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
        let stopHour = dateFormatter.stringFromDate(dateTo)
        let roomNumber = 418
        
        let reservation = Reservation(id: self.idNumber, date: day, startHour: startHour, stopHour: stopHour, roomNumber: roomNumber, comment: comment)
        saveReservationToServer(reservation)
        self.delegate.reservationReceived(reservation)
        self.navigationController?.popViewControllerAnimated(true)
        
    }
    
    // Runs asynchronously!!!
    private func saveReservationToServer(reservation: Reservation) {
        // Create a reference to a Firebase location
        let ref = Constants.RootReference.childByAppendingPath("reservations")
        // Write data to Firebase
        let idRef = ref.childByAppendingPath("\(reservation.id)")
        idRef.setValue(reservation.format())
    }
    
    // Return the next available index in the Firebase database
    private func fetchIdFromFireBase() {
        let ref = Constants.RootReference.childByAppendingPath("reservations")
        var count = 0
        ref.observeSingleEventOfType(.Value, withBlock: { snapshot in
            for _ in snapshot.children {
                count += 1
            }
            self.idNumber = count
        })
    }
    
    
    private struct Constants {
        static let RootReference = Firebase(url:"https://teknobyen.firebaseio.com")
    }
   
    

    

}
