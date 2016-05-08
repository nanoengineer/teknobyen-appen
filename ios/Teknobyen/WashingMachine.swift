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
    case Unknown
}

class WashingMachine
    
{
    var status: WashingMachineStatus
    var minutesRemaining: Int
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
            case .Unknown:
                return "STATUS UNKNOWN"
            }

        }
    }
    
    init(_id:Int, _status:WashingMachineStatus, _minutesRemaining: Int)
    {
        self.status = _status
        self.minutesRemaining = _minutesRemaining
        self.id = _id
    }
    
    init(_id: Int) {
        self.status = WashingMachineStatus.Unknown
        self.minutesRemaining = 0
        self.id = _id
    }
}
