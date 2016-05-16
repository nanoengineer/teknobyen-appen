//
//  SecureCredentials.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-16.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import Foundation
import Locksmith

struct TBCredentials: ReadableSecureStorable,
                      CreateableSecureStorable,
                      DeleteableSecureStorable,
                      GenericPasswordSecureStorable {
    
    var username:   String?
    var password:   String?
    var name:       String?
    var roomNumber: String?
    
    let service = "WashingMachine"
    var account: String { return service }
    
    var data: [String: AnyObject] {
        return ["password": password!,
                "username": username!,
                "name"    : name!,
            "roomNumber"  : roomNumber!]
    }
    
    init() {
        
    }
    
    mutating func credentialsFill ( data: [String:String] ) {
        self.name = data["name"]
        self.roomNumber = data["roomNumber"]
        self.username = data["username"]
        self.password = data["password"]
    }
}

var UserTBCredentials = TBCredentials()