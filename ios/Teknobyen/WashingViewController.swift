//
//  WashingViewController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-04-30.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import SafariServices

class WashingViewController: UIViewController {

   
    
    override func viewDidLoad() {
        super.viewDidLoad()


        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func checkMachineStatusPressed(sender: UIButton) {
    
        
        print("Opening Web")
        
        let machineStatusUrl = NSURL(string: "http://129.241.152.11/LaundryState?lg=2&ly=9131")
        
        
        openWithSafariVC(machineStatusUrl!)
        
    }
    
    func openWithSafariVC(url: NSURL)
    {
        let svc = SFSafariViewController(URL: url)
        self.presentViewController(svc, animated: true, completion: nil)
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
