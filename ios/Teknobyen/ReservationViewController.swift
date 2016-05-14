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
        
        loadReservationsFromServers()
       
    }

    func reserve() {
        self.performSegueWithIdentifier("Reserve", sender: self)
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        if segue.identifier == "Reserve" {
            if let rvc = segue.destinationViewController as? AddReservattionViewController {
                rvc.delegate = self
            }
        }
    }
    
    var reservations = [Reservation]()
    
    
    
    func reservationReceived(reservation: Reservation) {
        reservations.append(reservation)
        self.tableView.reloadData()
    }
    
    
    func loadReservationsFromServers() {
        let ref = Constants.RootReference.childByAppendingPath("reservations")
        ref.observeSingleEventOfType(.Value, withBlock: { snapshot in
            for stuff in snapshot.children {

                let userID = stuff.value["userID"] as! String
                let comment = stuff.value["comment"] as! String
                let name = stuff.value["name"] as! String
                
                let roomNumber = stuff.value["roomNumber"] as! String
                
                let date = stuff.value["date"] as! String
                let startTime = stuff.value["startTime"] as! String
                let duration = stuff.value["duration"] as! String

                let reservation = Reservation(  userID: userID,
                                                comment: comment,
                                                name: name,
                                                roomNumber: roomNumber,
                                                date: date,
                                                startTime: startTime,
                                                duration: duration)
                
                self.reservations.append(reservation)
            }
            self.tableView.reloadData()
        })
    }
    
    private struct Constants {
        static let RootReference = Firebase(url:"https://teknobyen.firebaseio.com")
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
        
        cell.nameLabel.text = reservation.name
        cell.dateLabel.text = reservation.date
        cell.roomLabel.text = "Rom \(reservation.roomNumber)"
        cell.hourLabel.text = "\(reservation.startTime) - \(reservation.endTime)"
        cell.commentLabel.text = reservation.comment

        return cell
    }
    
    override func tableView(tableView: UITableView, heightForRowAtIndexPath indexPath: NSIndexPath) -> CGFloat    {
        return 100.0;//Choose your custom row height
    }

  

}

protocol ReservationDelegate {
    func reservationReceived(reservation: Reservation)
}


struct Reservation {

    let userID: String
    let comment: String
    let name: String
    let roomNumber: String
    let date: String
    let startTime: String
    let duration: String
    
    var endTime: String {
        get {
            let dateFormatter = NSDateFormatter()
            dateFormatter.locale = NSLocale.init(localeIdentifier: "no_NO")
            dateFormatter.dateFormat = "HH.mm"
            
            let start = dateFormatter.dateFromString(startTime)
            let interval = NSTimeInterval.init(Float(duration)!*3600)
            let end = start!.dateByAddingTimeInterval(interval)
            return dateFormatter.stringFromDate(end)
        }
    }
    
    func format() -> [String: String] {
        return ["userID": userID, "comment": comment, "name": name, "roomNumber": "\(roomNumber)", "date": date, "startTime": startTime, "duration": duration]
    }
    
}
