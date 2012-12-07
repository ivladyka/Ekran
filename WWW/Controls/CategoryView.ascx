<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryView.ascx.cs" Inherits="CategoryView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="DatePicker" Src="ValueControls/DatePicker.ascx" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script language="javascript">
    function VIKKI_CheckInDateSelected(sender, e) {
        if (e.get_newDate() != null) {
            VIKKI_SetDataPicketValue($find("<%= dpCheckOutDate.DatePickerClientID %>"), e.get_newDate());
        }
    }

    function VIKKI_CheckInDateSelected1(sender, e) {
        if (e.get_newDate() != null) {
            VIKKI_SetDataPicketValue($find("<%= dpCheckOutDate1.DatePickerClientID %>"), e.get_newDate());
        }
    }

    function VIKKI_SetDataPicketValue(datePicker, date) {
        date.setDate(date.getDate() + 1); 
        datePicker.set_selectedDate(date); 
    }
</script>
</telerik:RadCodeBlock>
<div class="FooterTextContent">
            <h3><asp:Label ID="lblName" runat="server" Text=""></asp:Label></h3>
            <asp:Label ID="lblCategoryContent" runat="server" Text=""></asp:Label>
       <asp:Panel ID="pnlChildCategories" runat="server" Visible="false" Width="880px" >
               <ul style="list-style-type: none; list-style-image: none; overflow: hidden; float: left; display: inline;" Class="homepanel" >
                   <li style="list-style-type: none; list-style-image: none; overflow: hidden; float: left" >
                       <ul id="ulChildCategories" runat="server">
                       </ul>
                   </li>
               </ul>  
                </asp:Panel>
         <asp:Panel ID="pnlBooking" runat="server" Visible="false">
          <table border="0" width="880"  style="margin-left:40px;"  id="tablebookingpage">
              <tr>
                  <td colspan="5">
                      <asp:Label ID="lblPleaseFillInData" runat="server" Text="<%$Resources:Vikkisoft, PleaseFillInData %>" />

                  </td>
              </tr>
              <tr>
                  <td colspan="3" style="width:33%"><%=CheckInRes%><span>*</span></td>
                  <td  style="width:33%"><%=Name%><span>*</span></td>
                   <td><%=Room%></td>
              </tr>
                <tr>
                  <td colspan="3">  
                      <uc1:DatePicker id="dpCheckInDate1" runat="server" IsRequire="true" Width="160" ValidationGroup="BookingValidation" OnClientDateSelected="VIKKI_CheckInDateSelected1"></uc1:DatePicker> 
                  </td>
                    <td>
                       <ul style="list-style-type: none; list-style-image: none">
                           <li><asp:TextBox ID="tbName1" runat="server" Width="220px" Height="20px"  CssClass="inputstyle"></asp:TextBox></li>
                           <li> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbName1" Display="Dynamic" ValidationGroup="BookingValidation"></asp:RequiredFieldValidator></li>
                       </ul>
                    </td>
                     <td><telerik:RadComboBox ID="ddlRoomPrices" runat="server" Width="220px" EnableLoadOnDemand="false"></telerik:RadComboBox> </td>
              </tr>
                <tr>
                  <td colspan="3"><%=CheckOutRes%><span>*</span></td>
                    <td>E-mail:<span>*</span></td>
                    <td><%=Message%></td>
              </tr>
                 <tr>
                  <td colspan="3">
                    <uc1:DatePicker id="dpCheckOutDate1" runat="server" IsRequire="true" Width="160" ValidationGroup="BookingValidation"></uc1:DatePicker>  <br />
                    <asp:Label ID="dateCompare1" runat="server" Visible="False" Text="<%$Resources:Vikkisoft, DateCompareError %>"></asp:Label>
                  </td>
                      <td> 
                        <ul style="list-style-type: none; list-style-image: none">
                           <li><asp:TextBox ID="tbEmail1" runat="server" Width="220px" Height="20px"   CssClass="inputstyle"></asp:TextBox></li>
                            <li><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbEmail1" Display="Dynamic" ValidationGroup="BookingValidation"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$Resources:Vikkisoft, IncorrectEmail %>" ControlToValidate="tbEmail1" Display="Dynamic" ValidationGroup="BookingValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </li></ul>
                      </td>
                     <td rowspan="3"><asp:TextBox ID="tbMessage" runat="server" Width="220px" Height="70px" TextMode="MultiLine" CssClass="inputstyle"></asp:TextBox> </td>
              </tr>                
                 <tr>
                  <td style="width:10%"><%=RoomsRes%></td>
                  <td style="width:10%"><%=AdultsRes%></td>
                  <td><%=ChildrenRes%></td>
                  <td><%=Phone%></td>
              </tr>
              <tr>
                  <td>
                    <telerik:RadComboBox ID="ddlRooms1" runat="server" Width="60px" EnableLoadOnDemand="false" CssClass="homedropdown">
                      </telerik:RadComboBox> 
                  </td>
                  <td>
                    <telerik:RadComboBox ID="ddlAdults1" runat="server" Width="60px" EnableLoadOnDemand="false" CssClass="homedropdown">
                       </telerik:RadComboBox>
                  </td>
                  <td>
                    <telerik:RadComboBox ID="ddlChildren1" runat="server" Width="60px" EnableLoadOnDemand="false" CssClass="homedropdown">
                       </telerik:RadComboBox></td>
                  <td>
                      <asp:TextBox ID="tbPhone" runat="server" Width="220px" Height="20px"  CssClass="inputstyle"></asp:TextBox>                               
                  </td>
              </tr>
              <tr class="VIKKI_HiddenButton">
                <td colspan="5">
                    <asp:TextBox ID="tbCFH1" runat="server" Width="400px"  BackColor="White" CssClass="inputstyle"></asp:TextBox>
                </td>
               </tr>
                 <tr>
                  <td colspan="5"  style="padding-right: 70px">
                       <asp:Button ID="btnBookNow1" runat="server" OnClick="btnBookNow1_Click" Text="<%$Resources:Vikkisoft, BookNow %>" CssClass="formbutton" ValidationGroup="BookingValidation"/></td>
              </tr>
              <tr>
                <td colspan="5">
                    <div style="font-size:14pt; color:#ffc33f; font-weight:bold;">
                        <asp:Label ID="lbError1" runat="server" Text="" Visible="False"></asp:Label>
                    </div>
                </td>
              </tr>
          </table>
</asp:Panel>        
        <div >
        <asp:Panel ID="pnlHomeSection" runat="server" Visible="false" CssClass="homepanel">
            <ul>
        <li id="liAbourUS" runat="server" style="width:200px;">
            <a href="Default.aspx?content=CategoryView&CategoryID=28">
                <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/WEBSite/b914a224-8e63-4197-bf86-906b5364e9e5_s.jpg"/>
            
                <div class="subpagetitle">           
                   <h4><asp:Label ID="Label2" runat="server" Text="<%$Resources:Vikkisoft, AboutUs %>">:</asp:Label></h4>
                </div>
            </a>
      </li>
      <li id="liGallery" runat="server" style="width:200px;">
          <a href="Default.aspx?content=CategoryView&CategoryID=3">        
               <asp:Image ID="Image2" runat="server"  ImageUrl="~/Images/WEBSite/775352ca-2078-4b46-853b-2c831876263e_s.jpg"/>
               <div class="subpagetitle">           
                    <h4><asp:Label ID="Label1" runat="server" Text="<%$Resources:Vikkisoft, Gallery %>">:</asp:Label></h4>
                </div>
               </a>
      </li>
      <li id="liEvents" runat="server" style="width:200px;">
          <a href="Default.aspx?content=CategoryView&CategoryID=19">     
               <asp:Image ID="Image3" runat="server"  ImageUrl="~/Images/WEBSite/f3dbd0ad-697a-4aa6-b253-283e22c6109e_s.jpg"/>      
               <div class="subpagetitle">           
                   <h4><asp:Label ID="Label4" runat="server" Text="<%$Resources:Vikkisoft, Events %>">:</asp:Label></h4>
                </div>
               </a>
      </li>
      </ul>
         </asp:Panel>
        <asp:Panel ID="pnlGallery" runat="server" Visible="false">
        <div id="thumb-tray" class="load-item">
		    <div id="thumb-back"></div>
		    <div id="thumb-forward"></div>
	    </div>
        </asp:Panel>
        </div>
      
        <asp:Panel ID="pnlContactUS" runat="server" Visible="false">
        <table cellpadding="0" cellspacing="0"  border="0" style=" float:right; margin:0px 20px 0px 20px;" >
        <tr style="height:27px;">
            <td style="width: 110px; text-align:left;  padding-right:5px;">
                <asp:Label ID="lblYourName" runat="server" Text="<%$Resources:Vikkisoft, YourName %>"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding:0px;" >
                <asp:TextBox ID="tbName" runat="server" TabIndex="1" Width="240px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbName" Display="Dynamic" ValidationGroup="ContactUsValidation"></asp:RequiredFieldValidator>
            </td>
        </tr>                           
        <tr style="height:27px;">
            <td style="width: 110px; text-align:left;  padding-right:5px;">
                E-mail:</td>
            </tr>
        <tr>
            <td style="padding:0px;">
                <asp:TextBox ID="tbEmailAddress" runat="server" TabIndex="2" Width="240px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbEmailAddress" Display="Dynamic" ValidationGroup="ContactUsValidation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="<%$Resources:Vikkisoft, IncorrectEmail %>" ControlToValidate="tbEmailAddress" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ContactUsValidation"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style="height:27px;">
            <td style="width: 110px; text-align:left; vertical-align:top; padding-right:5px; padding-top:5px;">
                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Vikkisoft, Message %>">:</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding:0px;">
                <asp:TextBox ID="tdComments"  runat="server" TabIndex="6" Width="400px"  TextMode="MultiLine" Rows="7" MaxLength="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvComment" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tdComments" Display="Dynamic" ValidationGroup="ContactUsValidation"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="VIKKI_HiddenButton">
            <td>
            </td>
            <td>
                <asp:TextBox ID="tbCFH" runat="server" TabIndex="7" Width="400px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;  padding:10px 15px 0px 3px;">
                <asp:Button ID="btnSendMessage" runat="server" OnClick="btnSendMessage_Click" Text=" <%$Resources:Vikkisoft, SendMessage %> "  CssClass="formbutton" ValidationGroup="ContactUsValidation" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="font-size:10pt; color:#d60000; font-weight:bold;">
                    <asp:Label ID="lbError" runat="server" Text="" Visible="False"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
        </asp:Panel>
        <asp:Panel ID="pnlRoomPrice" runat="server" Visible="false">
          <div class="PriceRoomstyle"> 
            <asp:Label ID="lblPriceRoom" runat="server" Text="<%$Resources:Vikkisoft, RoomPrice %>" CssClass="PriceRoomTitle" />
            <table id="tblRoomPrices" runat="server" style="width: 200px;">
            </table>
          </div> 
        </asp:Panel>        

</div> 
<asp:Panel ID="pnlBookingHome" runat="server" Visible="false" CssClass="HomeBOOKING">
          <table border="0" style="width:170PX" >
              <tr>
                  <td style="padding-top:2px;"><%=CheckInRes%></td>
                  <td style="width:33%; text-align:center"><%=RoomsRes%></td>
                  <td style="width:33%; text-align:center"><%=AdultsRes%></td>
                  <td style="text-align:center"><%=ChildrenRes%></td>
              </tr>
              <tr>
                  <td> 
                    <uc1:DatePicker id="dpCheckInDate" runat="server" IsRequire="true" Width="170" ValidationGroup="HomeBOOKING" OnClientDateSelected="VIKKI_CheckInDateSelected"></uc1:DatePicker>
                  </td>
                  <td style="padding-left: 0px;">
                      <telerik:RadComboBox ID="ddlRooms" runat="server" Width="50px"  EnableLoadOnDemand="false" CssClass="homedropdown">
                      </telerik:RadComboBox> 
                  </td>
                  <td style="padding-left: 0px;">
                    <telerik:RadComboBox ID="ddlAdults" runat="server" Width="50px" EnableLoadOnDemand="false" CssClass="homedropdown">
                       </telerik:RadComboBox></td>
                  <td style="padding: 0px;">
                      <telerik:RadComboBox ID="ddlChildren" runat="server" Width="50px"  EnableLoadOnDemand="false" CssClass="homedropdown">                       
                      </telerik:RadComboBox>
                  </td>
              </tr>
              <tr>
                  <td><%=CheckOutRes%></td>
                  <td colspan="3" rowspan="2">
                    <asp:Button ID="btnBookNow" runat="server" OnClick="btnBookNow_Click" Text="<%$Resources:Vikkisoft, BookNow %>" ValidationGroup="HomeBOOKING" CssClass="formbutton" Width="160px" />
                  </td>
              </tr>
              <tr>
                  <td>
                    <uc1:DatePicker id="dpCheckOutDate" runat="server" IsRequire="true" Width="170" ValidationGroup="HomeBOOKING"></uc1:DatePicker>
                  </td>
              </tr> 
              <tr>
                  <td>  
                    <asp:Label ID="dateCompare" runat="server" Visible="False" ForeColor="Red" Text="<%$Resources:Vikkisoft, DateCompareError %>"></asp:Label>
                  </td>
                  <td colspan="3"></td>
              </tr>                
          </table>
</asp:Panel>