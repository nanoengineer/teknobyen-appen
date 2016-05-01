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
        self.navigationItem.title = "Teknobyen"
        self.navigationItem
    }
    
    override func viewWillAppear(animated: Bool) {
        self.navigationItem.title = "Teknobyen"
    }
    
    override func viewWillDisappear(animated: Bool) {
        self.navigationItem.title = ""
    }

    
    
    
   

}
