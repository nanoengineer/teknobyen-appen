//
//  washingStatusViewController.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-04.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import Fuzi

class WashingStatusViewController: UIViewController, UICollectionViewDelegate, UICollectionViewDataSource {
    
    @IBOutlet weak var washingMachineCollection: UICollectionView!
    
    @IBOutlet weak var collectionFlowLayout: UICollectionViewFlowLayout!
    
    
    var washingMachinesData = [WashingMachine]()
    
    var refreshControl: UIRefreshControl!
    
    override func viewDidLoad(){
        super.viewDidLoad()
        self.view.backgroundColor = UIColor.whiteColor()
        self.automaticallyAdjustsScrollViewInsets = false
        
        for index in 0...AppConstants.numOfWashingMachines-1 {
            washingMachinesData.append(WashingMachine(_id: index+2)) //machine's id starts at 2
        }
        
        self.collectionFlowLayout.sectionInset = UIEdgeInsets(top: AppConstants.varDefs.cellMargin, left: AppConstants.varDefs.cellMargin, bottom: AppConstants.varDefs.cellMargin, right:AppConstants.varDefs.cellMargin)
        
        self.collectionFlowLayout.itemSize = AppConstants.varDefs.cellSize

        washingMachineCollection.dataSource = self
        washingMachineCollection.delegate = self
        washingMachineCollection.registerClass(WashingMachineCell.self, forCellWithReuseIdentifier: "Cell")
        washingMachineCollection.backgroundColor = UIColor.whiteColor()
        washingMachineCollection.userInteractionEnabled = true
        washingMachineCollection.bounces = true
        washingMachineCollection.alwaysBounceVertical = true
        washingMachineCollection.allowsSelection = true
        
        refreshControl = UIRefreshControl()
        let attributes = [NSForegroundColorAttributeName: AppConstants.themeBlueColor]
        refreshControl.attributedTitle = NSAttributedString(string: "Oppdatering...", attributes: attributes)
        refreshControl.tintColor = AppConstants.themeBlueColor
        
        refreshControl.addTarget(self, action: #selector(WashingStatusViewController.pullToRefresh(_:)), forControlEvents: UIControlEvents.ValueChanged)

        self.view.addSubview(washingMachineCollection)
        washingMachineCollection.addSubview(refreshControl)

    }
    
    override func viewWillAppear(animated: Bool) {
        loadMachineStatusHtml()
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func pullToRefresh(sender:AnyObject) {
        loadMachineStatusHtml()
    }
    
    func collectionView(collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return AppConstants.numOfWashingMachines
    }
    
    func collectionView(collectionView: UICollectionView, cellForItemAtIndexPath indexPath: NSIndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCellWithReuseIdentifier("Cell", forIndexPath: indexPath) as! WashingMachineCell
        
        cell.cellImageView.image = UIImage(named: "washingMachineCellImage2")?.imageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
        cell.cellImageView.tintColor = UIColor.whiteColor().colorWithAlphaComponent(0.8)
        
        cell.machine = washingMachinesData[indexPath.row]
        
        cell.cellText.text = cell.machine!.displayString
        cell.backgroundColorUpdate()
        
        if indexPath.row == AppConstants.numOfWashingMachines - 1 {
            refreshControl.endRefreshing()
        }
    
        return cell
    }

    func collectionView(collectionView: UICollectionView, didDeselectItemAtIndexPath indexPath: NSIndexPath) {

    }
    func collectionView(collectionView: UICollectionView, didSelectItemAtIndexPath indexPath: NSIndexPath) {

    }
    
    private func loadMachineStatusHtml() {
        
        let username = UserTBCredentials.username!
        let pwd = UserTBCredentials.password!
        
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
                if data != nil {
                    
                    let htmlString = NSString(data: data!, encoding: NSUTF8StringEncoding)
                    do {
                        // if encoding is omitted, it defaults to NSUTF8StringEncoding
                        let doc = try HTMLDocument(string: String(htmlString), encoding: NSUTF8StringEncoding)
                        
                        var machineIndex:Int = 0
                        
                        for element in doc.xpath("//div[contains(@class, 'reservation')]/table//td[contains(@class, 'p')]/text()") {
                            
                            let elementString = String(element)
                            print("Maskin \(machineIndex + 2): " + elementString)
                            
                            if elementString == "Ledig" {
                                self.washingMachinesData[machineIndex].status = WashingMachineStatus.Available
                                self.washingMachinesData[machineIndex].minutesRemaining = 0
                            }
                            else if elementString.containsString("Resttid") {
                                let statusMin = String(element).substringWithRange(elementString.startIndex.advancedBy(9) ..< elementString.endIndex.advancedBy(-5))
                                self.washingMachinesData[machineIndex].status = WashingMachineStatus.Running
                                self.washingMachinesData[machineIndex].minutesRemaining = Int(statusMin)!
                            }
                            else if elementString.containsString("Ute av drift") {
                                self.washingMachinesData[machineIndex].status = WashingMachineStatus.OutOfOrder
                                self.washingMachinesData[machineIndex].minutesRemaining = 0
                            }
                            else if elementString.containsString("Ledig til") {
                                machineIndex -= 1
                                //Since "Ledig til" and the time string "kl. hh:mm" are separated, skip this string
                                //TODO: Pretty hacky solution for now, find a better one later!
                            }
                            else if elementString.containsString("kl.") {
                                self.washingMachinesData[machineIndex].status = WashingMachineStatus.Reserved
                                self.washingMachinesData[machineIndex].minutesRemaining = 0
                                self.washingMachinesData[machineIndex].reservationTime = elementString.capitalizedString
                            }
                            else {
                                self.washingMachinesData[machineIndex].status = WashingMachineStatus.Unknown
                            }
                            machineIndex += 1
                        }
                        
                        dispatch_async(dispatch_get_main_queue(),{
                            self.washingMachineCollection.reloadData()
                        })
                        
                    }
                    catch let error {
                        print(error)
                    }
                }
                else {
                    print("Error: \(error)): No internet connection!")
                    print("Reponse: \(response)")
                    self.washingMachineCollection.reloadData()
                }
        }
        task.resume()
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