//
//  WashingRefillViewController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-05.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import SafariServices
import Fuzi

class WashingRefillViewController: UIViewController, SFSafariViewControllerDelegate {
    
    
    @IBOutlet weak var balanceLabel: UILabel!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        balanceLabel.layer.cornerRadius = 7
        
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
    
    private func updateBalanceLabel(string: String) {
        self.balanceLabel.text = string
    }
    
    private func loadWebPage() {
        let username = UserTBCredentials.username!
        let pwd = UserTBCredentials.password!
        
        let config = NSURLSessionConfiguration.defaultSessionConfiguration()
        let userPasswordString = "\(username):\(pwd)"
        let userPasswordData = userPasswordString.dataUsingEncoding(NSUTF8StringEncoding)
        let base64EncodedCredential = userPasswordData!.base64EncodedStringWithOptions(NSDataBase64EncodingOptions.Encoding64CharacterLineLength)
        let authString = "Basic \(base64EncodedCredential)"
        config.HTTPAdditionalHeaders = ["Authorization" : authString]
        let session = NSURLSession(configuration: config)
        
        let url = NSURL(string: "http://129.241.152.11/SaldoForm?lg=2&ly=9131")
        let task = session.dataTaskWithURL(url!) {
            (let data, let response, let error) in
            if data != nil {
                let htmlString = NSString(data: data!, encoding: NSASCIIStringEncoding) //WTF ARE YOU SERIOUS
                
                do {
                    // if encoding is omitted, it defaults to NSUTF8StringEncoding
                    let doc = try HTMLDocument(string: String(htmlString), encoding: NSUTF8StringEncoding)
                    
                    var textFlag = false
                    var balanceString = ""
                    
                    for element in doc.xpath("//div[contains(@class, 'reservation')]/table//td[contains(@class, 'p')]/text()") {
                        
                        if textFlag == true {
                            balanceString = balanceString + "       " + String(element)
                            textFlag = false
                        }
                        
                        if String(element) == "BALANSE" {
                            textFlag = true
                            balanceString = String(element) + ":"
                        }
                        
                        dispatch_async(dispatch_get_main_queue(),{
                            self.updateBalanceLabel(balanceString)
                        })
                        
                    }
                    
                }
                catch let error {
                    print(error)
                }
            }
            else {
                print("Error: \(error)): No internet connection!")
                print("Reponse: \(response)")
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
