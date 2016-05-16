//
//  TBNavigationController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-07.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit

class TBNavigationController: UINavigationController {

    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
        initDefaultAppearances()
        initCredentials()
    }
    
    private func initDefaultAppearances() {
        UILabel.appearanceWhenContainedInInstancesOfClasses([UIButton.self]).font = AppConstants.normalFont! //Doesn't seem to be of any effect...
        UILabel.appearance().font = AppConstants.normalFont
        UITextField.appearance().font = AppConstants.normalFont
    }
    
    private func initCredentials() {
        if let result = UserTBCredentials.readFromSecureStore() {
            UserTBCredentials.credentialsFill(result.data as! [String:String])
            print(UserTBCredentials)
        }
        else {
            print("No credentials retrived")
        }
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
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
