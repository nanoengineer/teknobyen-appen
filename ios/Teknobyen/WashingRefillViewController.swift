//
//  WashingRefillViewController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-05.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import SafariServices

class WashingRefillViewController: UIViewController, SFSafariViewControllerDelegate {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    }
    
    override func viewWillAppear(animated: Bool) {
        loadWebPage()
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    private func openWithSafariVC(url: NSURL)
    {
        let svc = SFSafariViewController(URL: url)
        svc.delegate = self
        self.presentViewController(svc, animated: true, completion: nil)
    }
    
    func safariViewControllerDidFinish(controller: SFSafariViewController)
    {
       
    }
    
    
    private func loadWebPage() {
        let username = "pkminne"
        let pwd = "b5e277"
        
        let config = NSURLSessionConfiguration.defaultSessionConfiguration()
        let userPasswordString = "\(username):\(pwd)"
        let userPasswordData = userPasswordString.dataUsingEncoding(NSUTF8StringEncoding)
        let base64EncodedCredential = userPasswordData!.base64EncodedStringWithOptions(NSDataBase64EncodingOptions.Encoding64CharacterLineLength)
        let authString = "Basic \(base64EncodedCredential)"
        config.HTTPAdditionalHeaders = ["Authorization" : authString]
        let session = NSURLSession(configuration: config)
        
        let url = NSURL(string: "http://129.241.152.11/AccountPayment?lg=2&ly=9131")
        let task = session.dataTaskWithURL(url!) {
            (let data, let response, let error) in
            if let httpResponse = response as? NSHTTPURLResponse {
                print(httpResponse.statusCode.description)
                print(data)
                print(error)
                self.openWithSafariVC(url!)
            }
            
        }
        task.resume()
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
