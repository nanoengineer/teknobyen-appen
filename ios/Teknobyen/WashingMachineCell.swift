//
//  WashingMachineTile.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-06.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import Foundation
import UIKit

class WashingMachineCell: UICollectionViewCell {
    
    let cellColor = AppConstants.machineAvailColor
    var cellImageView: UIImageView!
    var cellText: UILabel!
    var cellSubscribeButton: UIButton?
    
    var machine: WashingMachine?
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        self.cellImageView = UIImageView(frame: CGRect(x: 0, y: 40, width: frame.size.width, height: frame.size.height*(0.4)))
        self.cellImageView.contentMode = UIViewContentMode.ScaleAspectFit
        super.contentView.addSubview(cellImageView)
        
        self.cellText = UILabel(frame: CGRect(x: 0, y: cellImageView!.frame.size.height + 40, width: frame.size.width, height: frame.size.height/3))
        self.cellText.font = UIFont.systemFontOfSize(14)
        self.cellText.textAlignment = .Center
        self.cellText.backgroundColor = UIColor.clearColor()
        self.cellText.textColor = UIColor.whiteColor()
        self.contentView.addSubview(cellText)
        self.backgroundColor = AppConstants.machineAvailColor
    }
    
    func backgroundColorUpdate() {
        if let wshmachine = self.machine {
            switch wshmachine.status {
            case .Available:
                self.backgroundColor = AppConstants.machineAvailColor
            case .Running:
                self.backgroundColor = AppConstants.machineBusyColor
            case .Reserved:
                self.backgroundColor = AppConstants.machineAvailColor
            case .OutOfOrder:
                self.backgroundColor = AppConstants.machineBusyColor
            case .Unknown:
                self.backgroundColor = AppConstants.machineUnknownColor
            }
        }

    }
    
    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    

}