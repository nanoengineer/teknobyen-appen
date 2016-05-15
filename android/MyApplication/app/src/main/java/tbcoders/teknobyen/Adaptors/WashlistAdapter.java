package tbcoders.teknobyen.adaptors;

import android.app.Activity;
import android.content.Context;
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
public class WashlistAdapter extends ArrayAdapter<Washdays>{

    Context context;
    int layoutResourceId;
    ArrayList<Washdays> data;

    public WashlistAdapter(Context context, int layoutResourceId, ArrayList<Washdays> data) {
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
            holder.txtDate = (TextView) row.findViewById(R.id.txtDate);
            holder.txtAssignment = (TextView)row.findViewById(R.id.txtAssignment);
            holder.txtRoom = (TextView)row.findViewById(R.id.txtRoom);

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
        return row;
    }

    static class WashdaysHolder
    {
        TextView txtDate;
        TextView txtAssignment;
        TextView txtRoom;
    }
}