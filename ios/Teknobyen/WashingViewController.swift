//
//  WashingViewController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-04-30.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import SafariServices
import Fuzi

class WashingViewController: UIViewController, NSURLConnectionDelegate {

   
    @IBOutlet weak var washWebView: UIWebView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.navigationItem.title = "Vaskemaskiner"
        washWebView.hidden = true

        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func checkMachineStatusPressed(sender: UIButton) {
        
        var htmlString: NSString?
        
        let username = "pkminne"
        let pwd = "b5e277"
        
        let config = NSURLSessionConfiguration.defaultSessionConfiguration()
        let userPasswordString = "\(username):\(pwd)"
        let userPasswordData = userPasswordString.dataUsingEncoding(NSUTF8StringEncoding)
        let base64EncodedCredential = userPasswordData!.base64EncodedStringWithOptions(NSDataBase64EncodingOptions.Encoding64CharacterLineLength)
        let authString = "Basic \(base64EncodedCredential)"
        config.HTTPAdditionalHeaders = ["Authorization" : authString]
        let session = NSURLSession(configuration: config)
        
        let url = NSURL(string: "http://129.241.152.11/LaundryState?lg=2&ly=9131")
        let task = session.dataTaskWithURL(url!) {
            (let data, let response, let error) in
            if let httpResponse = response as? NSHTTPURLResponse {
                htmlString = NSString(data: data!, encoding: NSUTF8StringEncoding)!
                
                do {
                    // if encoding is omitted, it defaults to NSUTF8StringEncoding
                    let doc = try HTMLDocument(string: String(htmlString), encoding: NSUTF8StringEncoding)
                    
                    for element in doc.xpath("//div[contains(@class, 'reservation')]/table//td[contains(@class, 'p')]/text()") {
                        print(element)
                    }
                    
                } catch let error {
                    print(error)
                }
                
            }
            
        }
        task.resume()
    }
    
    
    @IBAction func refillPressed(sender: UIButton) {
        
        let refillUrl = NSURL(string: "http://129.241.152.11/AccountPayment?lg=2&ly=9131")
        openWithSafariVC(refillUrl!)
        
    }
    
    private func openWithSafariVC(url: NSURL)
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
