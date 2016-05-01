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
        self.view.backgroundColor = .whiteColor()
        initNavigationBar()

    }
    
    private func initNavigationBar() {
        self.navigationItem.title = "TEKNOBYEN"
        self.navigationController?.navigationBar.barTintColor = UIColor.orangeColor()
    }
}