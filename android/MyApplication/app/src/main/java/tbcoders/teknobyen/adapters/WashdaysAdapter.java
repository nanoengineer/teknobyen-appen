package tbcoders.teknobyen.adapters;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.ArrayList;

import tbcoders.teknobyen.R;
import tbcoders.teknobyen.firebase.classes.Washdays;

/**
 * Created by Alexander on 15/05/2016.
 */
public class WashdaysAdapter extends ArrayAdapter<Washdays>{

    Context context;
    int layoutResourceId;
    ArrayList<Washdays> data;

    public WashdaysAdapter(Context context, int layoutResourceId, ArrayList<Washdays> data) {
        super(context, layoutResourceId, data);
        this.layoutResourceId = layoutResourceId;
        this.context = context;
        this.data = data;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View row = convertView;
        WashdaysHolder holder = null;

        if(row == null)
        {
            LayoutInflater inflater = ((Activity)context).getLayoutInflater();
            row = inflater.inflate(layoutResourceId, parent, false);

            holder = new WashdaysHolder();
            holder.txtDate = (TextView) row.findViewById(R.id.txtWashlistDate);
            holder.txtAssignment = (TextView)row.findViewById(R.id.txtWashlistAssignment);
            holder.txtRoom = (TextView)row.findViewById(R.id.txtWashlistRoom);

            row.setTag(holder);
        }
        else
        {
            holder = (WashdaysHolder)row.getTag();
        }

        Washdays weather = data.get(position);
        holder.txtDate.setText(weather.getDate());
        holder.txtAssignment.setText(weather.getAssignment().toString());
        holder.txtRoom.setText(weather.getRoomNumber().toString());

        if (position % 2 == 0) {
            row.setBackgroundColor(Color.WHITE);
        } else {
            row.setBackgroundColor(Color.LTGRAY);
        }
        return row;
    }

    static class WashdaysHolder
    {
        TextView txtDate;
        TextView txtAssignment;
        TextView txtRoom;
    }
}