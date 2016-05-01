//
//  ReservatorViewController.swift
//  Teknobyen
//
//  Created by Mathias Breistein on 30.04.2016.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit

class ReservatorViewController: UIViewController, UIPickerViewDelegate, UIPickerViewDataSource, UITextViewDelegate {

    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.view.backgroundColor = .yellowColor()
        populateDataSource()
        self.pickerView.dataSource = self;
        self.pickerView.delegate = self;
        commentView.text = ""
    }
    
    var delegate: ReservationDelegate!
    
    var pickerDataSource: [[String]]!
    
    private func populateDataSource() {
        var temp = [String]()
        for i in 0...23 {
            if i < 10 {
                temp.append("0\(i).00")
            } else {
                temp.append("\(i).00")
            }
            
        }
        pickerDataSource = [["Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag", "Søndag"], temp, temp]
    }

    @IBAction func reservationComplete(sender: UIButton) {
        let comment = commentView.text
        let day = pickerDataSource[0][pickerView.selectedRowInComponent(0)]
        let startHour = pickerDataSource[1][pickerView.selectedRowInComponent(1)]
        let stopHour = pickerDataSource[2][pickerView.selectedRowInComponent(2)]
        let roomNumber = 418
        
        let reservation = Reservation(date: day, startHour: startHour, stopHour: stopHour, roomNumber: roomNumber, comment: comment)
        self.delegate.reservationReceived(reservation)
        self.navigationController?.popViewControllerAnimated(true)
    }
    
    // MARK: Delegate methods
    func numberOfComponentsInPickerView(pickerView: UIPickerView) -> Int {
        return 3
    }
    
    func pickerView(pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return pickerDataSource[component].count;
    }
    
    func pickerView(pickerView: UIPickerView, titleForRow row: Int, forComponent component: Int) -> String? {
        return pickerDataSource[component][row]
    }
    
    func textView(textView: UITextView, shouldChangeTextInRange range: NSRange, replacementText text: String) -> Bool {
        if(text == "\n") {
            textView.resignFirstResponder()
            return false
        }
        return true
    }
   
    @IBOutlet weak var pickerView: UIPickerView!
    @IBOutlet weak var commentView: UITextView!

    

}
