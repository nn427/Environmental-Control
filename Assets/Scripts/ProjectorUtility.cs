using rv;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectorUtility : MonoBehaviour {
    private readonly string KEY_ROUTERIP = "ProjIP";

    [SerializeField] InputField input_IP;

    private void Start() {
        if (PlayerPrefs.HasKey(KEY_ROUTERIP)) {
            input_IP.text = PlayerPrefs.GetString(KEY_ROUTERIP);
        }
    }

    public void AwakeProj() {
        proj_Power(PowerCommand.Power.ON);
    }

    public void CloseProj() {
        proj_Power(PowerCommand.Power.OFF);
    }

    private void proj_Power(PowerCommand.Power cmd) {
        System.Net.IPAddress address = null;
        if (System.Net.IPAddress.TryParse(input_IP.text, out address)) {
            PlayerPrefs.SetString(KEY_ROUTERIP, input_IP.text);
            PJLinkConnection c = new PJLinkConnection(input_IP.text);
            PowerCommand pc = new PowerCommand(cmd);
            c.sendCommand(pc);
        } else {
        }
    }

    private void examples() {
        PJLinkConnection c = new PJLinkConnection(input_IP.text);

        //shortcuts
        //c.turnOn();
        //System.Console.WriteLine(c.getProjectorInfo()); 

        //detailed command calls

        //PowerCommand pc1 = new PowerCommand(PowerCommand.Powr.QUERY);
        //if (c.sendCommand(pc1) == Command.Response.SUCCESS)
        //    Console.WriteLine("Projector is " + pc1.Status.ToString());
        //else
        //    Console.WriteLine("Communication Error");

        //PowerCommand pc2 = new PowerCommand(PowerCommand.Power.ON);
        //if (c.sendCommand(pc2) == Command.Response.SUCCESS)
        //    Console.WriteLine("Switching on successful");
        //else
        //    Console.WriteLine("Communication Error");

        PowerCommand pc3 = new PowerCommand(PowerCommand.Power.OFF);
        if (c.sendCommand(pc3) == Command.Response.SUCCESS)
            Console.WriteLine("Projector is " + pc3.Status.ToString());
        else
            Console.WriteLine("Communication Error");

        //ErrorStatusCommand esc = new ErrorStatusCommand();
        //if (c.sendCommand(esc) == Command.Response.SUCCESS)            
        //    Console.WriteLine(esc.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //LampStatusCommand lsc = new LampStatusCommand();
        //if (c.sendCommand(lsc) == Command.Response.SUCCESS)            
        //    Console.WriteLine(lsc.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //ProjectorNameCommand pnc = new ProjectorNameCommand();
        //if (c.sendCommand(pnc) == Command.Response.SUCCESS)           
        //    Console.WriteLine(pnc.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //ManufacturerNameCommand mnc = new ManufacturerNameCommand();
        //if (c.sendCommand(mnc) == Command.Response.SUCCESS)            
        //    Console.WriteLine(mnc.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //ProductNameCommand prnc = new ProductNameCommand();
        //if (c.sendCommand(prnc) == Command.Response.SUCCESS)            
        //    Console.WriteLine(prnc.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //OtherInfoCommand oic = new OtherInfoCommand();
        //if (c.sendCommand(oic) == Command.Response.SUCCESS)            
        //    Console.WriteLine(oic.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //InputCommand ic1 = new InputCommand();
        //if (c.sendCommand(ic1) == Command.Response.SUCCESS)            
        //    Console.WriteLine(ic1.dumpToString());            
        //else
        //    Console.WriteLine("Communication Error");

        //InputCommand ic2 = new InputCommand(InputCommand.InputType.RGB, 2); 
        //if (c.sendCommand(ic2) == Command.Response.SUCCESS)
        //    Console.WriteLine(ic2.dumpToString());
        //else
        //    Console.WriteLine("Communication Error");

        ProjectorInfo pi = ProjectorInfo.create(c);
        string s = pi.toXmlString();
        Console.WriteLine(s);

        Console.ReadKey();
    }
}
