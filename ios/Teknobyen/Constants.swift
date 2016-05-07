//
//  Constants.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-05.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import Foundation
import UIKit


var AppConstants = Constants()

struct Constants {
    let titleString = "TEKNOBYEN"
    var themeColor = UIColor(red: 56/255, green: 171/255, blue: 209/255, alpha: 1)
    let tabNormalColor = UIColor.whiteColor()
    var tileColor:UIColor {
        get {
            return themeColor.colorWithAlphaComponent(0.5)
        }
    }
    let numOfWashingMachines = 6
    let washingMachineStartId: UInt =  4
    
    var varDefs = VarDefinitions()
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