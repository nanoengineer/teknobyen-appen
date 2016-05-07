////: Playground - noun: a place where people can play
//import Foundation
//
////let config = NSURLSessionConfiguration.defaultSessionConfiguration()
////let userPasswordString = "username@gmail.com:miele%20magic:password"
////let userPasswordData = userPasswordString.dataUsingEncoding(NSUTF8StringEncoding)
////let md5Credential = userPasswordData!.md5me() //TODO: Implement md5me method
////let authString = "Digest \(md5Credential)"
////config.HTTPAdditionalHeaders = ["Authorization" : authString]
////let session = NSURLSession(configuration: config)
////
////var running = false
////let url = NSURL(string: "http://129.241.152.11/LaundryState?lg=2&ly=9131")
////let task = session.dataTaskWithURL(url) {
////    (let data, let response, let error) in
////    if let httpResponse = response as? NSHTTPURLResponse {
////        let dataString = NSString(data: data, encoding: NSUTF8StringEncoding)
////        println(dataString)
////    }
////    running = false
////}
////
////running = true
////task.resume()
////
////while running {
////    print("waiting...")
////    sleep(1)
////}
//
//import Foundation
//import UIKit
//
//let config = NSURLSessionConfiguration.defaultSessionConfiguration()
//let userPasswordString = "pkminne:b5e277"
//let userPasswordData = userPasswordString.dataUsingEncoding(NSUTF8StringEncoding)
//let base64EncodedCredential = userPasswordData!.base64EncodedStringWithOptions(NSDataBase64EncodingOptions.Encoding64CharacterLineLength)
//let authString = "Basic \(base64EncodedCredential)"
//config.HTTPAdditionalHeaders = ["Authorization" : authString]
//let session = NSURLSession(configuration: config)
//
//var running = false
//let url = NSURL(string: "http://129.241.152.11/LaundryState?lg=2&ly=9131")
//let task = session.dataTaskWithURL(url!) {
//    (let data, let response, let error) in
//    if let httpResponse = response as? NSHTTPURLResponse {
//        let dataString = NSString(data: data!, encoding: NSUTF8StringEncoding)
//        print(dataString)
//        
//        var testWebView: UIWebView!
//        
//        testWebView.loadHTMLString(String(dataString), baseURL: nil)
//    }
//    running = false
//}
//
//
//
//running = true
//task.resume()
//
//while running {
//    print("waiting...")
//    sleep(1)
//}

import UIKit

var restid = "Resttid: 14 min"
var minutes = restid.substringWithRange(Range<String.Index>(start: restid.startIndex.advancedBy(9), end: restid.endIndex.advancedBy(-4)))


