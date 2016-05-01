//
//  ReservationViewController.swift
//  Teknobyen
//
//  Created by Mathias Breistein on 30.04.2016.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import Firebase

/* This class lets the user view what reservations are currently here,
 and also supports adding reservations.
 It uses FireBase to fetch and write reservations
 */

class ReservationViewController: UITableViewController, ReservationDelegate {
    
  
    

    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.navigationItem.title = "Reserver prosjektor"
        // Add a 'reserve'-button to the navigation bar
        let addButton = UIBarButtonItem(barButtonSystemItem: .Add, target: self, action: #selector(ReservationViewController.reserve))
        navigationItem.rightBarButtonItem = addButton
        navigationController?.navigationBar.backItem?.title = ""
       
    }

    func reserve() {
        saveReservationsToServers()
        // self.performSegueWithIdentifier("Reserve", sender: self)
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "Reserve" {
            if let rvc = segue.destinationViewController as? ReservatorViewController {
                rvc.delegate = self
            }
        }
    }
    

    func saveReservationsToServers() {
        // Create a reference to a Firebase location
        let myRootRef = Firebase(url:"https://teknobyen.firebaseio.com")
        let ref = myRootRef.childByAppendingPath("reservations")
        // Write data to Firebase
        for reservation in reservations {
            let idRef = ref.childByAppendingPath("\(reservation.id)")
            idRef.setValue(reservation.format())
        }
        
    }
    
    func loadReservationsFromServers() {
        
    }
    
    

    // MARK: - Table view data source

    override func numberOfSectionsInTableView(tableView: UITableView) -> Int {
        // #warning Incomplete implementation, return the number of sections
        return 1
    }

    override func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // #warning Incomplete implementation, return the number of rows
        return reservations.count
    }

    
    override func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCellWithIdentifier("ReservationCell", forIndexPath: indexPath) as! ReservationTableViewCell
        let reservation = reservations[indexPath.row]
        
        cell.dateLabel.text = reservation.date
        cell.roomLabel.text = "Rom \(reservation.roomNumber)"
        cell.hourLabel.text = "Fra \(reservation.startHour) til \(reservation.stopHour)"
        cell.commentLabel.text = reservation.comment

        return cell
    }
    
    override func tableView(tableView: UITableView, heightForRowAtIndexPath indexPath: NSIndexPath) -> CGFloat    {
        return 100.0;//Choose your custom row height
    }

    var reservations = [Reservation(id: 1, date: "Lørdag", startHour: "20:00", stopHour: "22:00", roomNumber: 418, comment: "Skal sjå Breaking bad"),
                        Reservation(id: 2, date: "Søndag", startHour: "00:00", stopHour: "02:00", roomNumber: 606, comment: "Det vert porno i natt!")]
    
   
    
    func reservationReceived(reservation: Reservation) {
        reservations.append(reservation)
        self.tableView.reloadData()
    }

}

protocol ReservationDelegate {
    func reservationReceived(reservation: Reservation)
}


struct Reservation {
    let id: Int
    let date: String
    let startHour: String
    let stopHour: String
    let roomNumber: Int
    let comment: String
    
    func format() -> [String: String] {
        return ["id": "\(id)","date": date, "startHour": startHour, "stopHour": stopHour, "roomNumber": "\(roomNumber)", "comment": comment]
    }
    
}
