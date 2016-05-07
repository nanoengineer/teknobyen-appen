//
//  WashingTabBarController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-04.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit


class WashingTabBarController: UITabBarController, UITabBarControllerDelegate {

    let washingStatusVC = WashingStatusViewController()
    let washingRefillVC = WashingRefillViewController()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.delegate = self
        AppConstants.varDefs.tabBarHeight = self.tabBar.frame.size.height
        self.viewControllers = [washingStatusVC, washingRefillVC]
        self.navigationItem.title = "Vaskemaskiner"
        
        statusTabBarItemSetUp()
        refillTabBarItemSetUp()

        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    private func statusTabBarItemSetUp() {
        let statusTab = UITabBarItem(title: "Status", image: UIImage(named: "washingTabBarStatus"), tag: 0)
        self.viewControllers![0].tabBarItem  = statusTab
        statusTab.setTitleTextAttributes([NSForegroundColorAttributeName : AppConstants.tabNormalColor], forState: UIControlState.Normal)
        self.tabBar.barTintColor = AppConstants.themeColor
    }
    
    private func refillTabBarItemSetUp() {
        let refillTab = UITabBarItem(title: "Påfyll", image: UIImage(named: "washingTabBarRefill"), tag: 1)
        self.viewControllers![1].tabBarItem = refillTab
        refillTab.setTitleTextAttributes([NSForegroundColorAttributeName : AppConstants.tabNormalColor], forState: UIControlState.Normal)
        self.tabBar.barTintColor = AppConstants.themeColor
    }
    
    func tabBarController(tabBarController: UITabBarController, didSelectViewController viewController: UIViewController) {
        if viewController == self.viewControllers![0] {
            print("Status")
            washingStatusVC.loadMachineStatusHtml()
        }
        else {
            print("Påfyll")
  
        }
    }
    
    override func shouldAutorotate() -> Bool {
        return false
    }
    
    override func supportedInterfaceOrientations() -> UIInterfaceOrientationMask {
        return UIInterfaceOrientationMask.Portrait
    }
    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */

}
