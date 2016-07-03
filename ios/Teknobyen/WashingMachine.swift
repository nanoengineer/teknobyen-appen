//
//  WashingMachine.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-04.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import Foundation

enum WashingMachineStatus
{
    case Available
    case Running
    case OutOfOrder
    case Reserved
    case Unknown
}

class WashingMachine
    
{
    var status: WashingMachineStatus
    var minutesRemaining: Int
    var reservationTime: String
    var isSubscribed = false
    private var id: Int
    var displayString: String
    {
        get
        {
            switch status
            {
            case .Available:
                return "LEDIG"
            case .Running:
                return "\(minutesRemaining) MIN"
            case .OutOfOrder:
                return "UTE AV DRIFT"
            case .Reserved:
                return "LEDIG TIL " + reservationTime
            case .Unknown:
                return "STATUS UKJENT"
            }
        }
    }
    
    init(_id:Int, _status:WashingMachineStatus, _minutesRemaining: Int)
    {
        self.status = _status
        self.minutesRemaining = _minutesRemaining
        self.id = _id
        self.reservationTime = ""
        
    }
    
    init(_id: Int) {
        self.status = WashingMachineStatus.Unknown
        self.minutesRemaining = 0
        self.id = _id
        self.reservationTime = ""
    }
}
