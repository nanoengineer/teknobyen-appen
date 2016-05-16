//
//  Constants.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-05.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import Foundation
import UIKit
import UIColor_Hex_Swift



var AppConstants = Constants()

struct Constants {
    let titleString = "TEKNOBYEN"

    let themeBlueColor = UIColor(rgba: "#7DCAE3FF")
    let themeRedColor = UIColor(rgba: "#E3967DFF")
    let themeGreenColor = UIColor(rgba: "#97E37DFF")
    let themePurpleColor = UIColor(rgba: "#C97DE3FF")
    
    
    
    let tabNormalColor = UIColor.grayColor()
    let tabSelectedColor = UIColor.blueColor().colorWithAlphaComponent(0.4)
    
    let machineAvailColor = UIColor(rgba: "#97E37DD5")
    let machineBusyColor = UIColor(rgba: "#E3967DC5")
    let machineUnknownColor = UIColor(rgba: "#E3D059C5")
    
    let numOfWashingMachines = 6
    let washingMachineStartId: UInt =  2
    
    let projectorBookingToggleText = durationDateText()

    let normalFont = UIFont(name: "Avenir Next", size: 17)
    let boldFont = UIFont(name: "AvenirNext-DemiBold", size: 17)

    var normalTextAttributes: [String : AnyObject] {
        get {
           return [NSForegroundColorAttributeName: UIColor.whiteColor(),
             NSFontAttributeName: self.normalFont!]
        }
    }

    var boldTextAttributes: [String : AnyObject] {
        get {
            return [NSForegroundColorAttributeName: UIColor.whiteColor(),
                    NSFontAttributeName: self.boldFont!]
        }
    }
    
    var varDefs = VarDefinitions()
}


struct durationDateText {
    let duration = "Velg varighet"
    let date     = "Velg dato"
}

struct VarDefinitions {
    var tabBarHeight: CGFloat = 0
    var navBarHeight: CGFloat = 0
    var cellMargin: CGFloat = 10
    var cellSize: CGSize {
        get {
            let screenSize: CGRect = UIScreen.mainScreen().bounds
            
            let screenWidth = screenSize.width
            let screenHeight = screenSize.height
            
            let usableScreenSize = CGSize(width: screenWidth, height: screenHeight - AppConstants.varDefs.tabBarHeight - AppConstants.varDefs.navBarHeight - 20)
            //not sure why I need the extra 20 here, I think maybe to account for the top status bar?
            
            return CGSize(width: (usableScreenSize.width - 3*cellMargin)/2, height: (usableScreenSize.height - 4*cellMargin)/3)
        }
    }
}