//
//  ReservationTableViewCell.swift
//  Teknobyen
//
//  Created by Mathias Breistein on 30.04.2016.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit

class ReservationTableViewCell: UITableViewCell {

    var reservation: Reservation!

    override func awakeFromNib() {
        super.awakeFromNib()
        self.backgroundColor = .whiteColor()


    }

    override func setSelected(selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

    @IBOutlet weak var dateLabel: UILabel!
    @IBOutlet weak var roomLabel: UILabel!
    @IBOutlet weak var hourLabel: UILabel!
    @IBOutlet weak var commentLabel: UILabel!

}
