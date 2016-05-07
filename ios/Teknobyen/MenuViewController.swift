//
//  MenuViewController.swift
//  Teknobyen
//
//  Created by Mathias Breistein on 30.04.2016.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit

class MenuViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        initNavigationBar()
    }
    
    private func initNavigationBar() {
        self.navigationController?.navigationBar.barTintColor = AppConstants.themeColor
        AppConstants.varDefs.navBarHeight = (self.navigationController?.navigationBar.frame.size.height)!
    }
    
    override func viewWillAppear(animated: Bool) {
        self.navigationItem.title = AppConstants.titleString
    }
    
    override func viewWillDisappear(animated: Bool) {
        self.navigationItem.title = ""
    }
    
    override func shouldAutorotate() -> Bool {
        return false
    }
    
    override func supportedInterfaceOrientations() -> UIInterfaceOrientationMask {
        return UIInterfaceOrientationMask.Portrait
    }
}
