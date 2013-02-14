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
    <ul>
        <li>
  <asp:Panel ID="pnlChildCategories" runat="server" Visible="false" CssClass="ChildCategories"  >

                       <ul id="ulChildCategories" runat="server" Class="ChildCategoriespanel">
                       </ul>
                   
  </asp:Panel>
  <asp:Panel ID="pnlHomeSection" runat="server" Visible="false" CssClass="homepanel" HorizontalAlign="right">               
            <ul>
        <li id="liAbourUS" runat="server">
            <a id="aAboutUs" runat="server" href="Default.aspx?content=CategoryView&CategoryID=28">
                <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/WEBSite/b914a224-8e63-4197-bf86-906b5364e9e5_s.jpg"/>            
                <div class="subpagetitle">           
                   <h4><asp:Label ID="Label2" runat="server" Text="<%$Resources:Vikkisoft, AboutUs %>">:</asp:Label></h4>
                </div>
            </a>
      </li>
      <li id="liGallery" runat="server">
          <a id="aGallery" runat="server" href="Default.aspx?content=CategoryView&CategoryID=3">        
               <asp:Image ID="Image2" runat="server"  ImageUrl="~/Images/WEBSite/775352ca-2078-4b46-853b-2c831876263e_s.jpg"/>
               <div class="subpagetitle">           
                    <h4><asp:Label ID="Label1" runat="server" Text="<%$Resources:Vikkisoft, Gallery %>">:</asp:Label></h4>
                </div>
         </a>
      </li>
      <li id="liEvents" runat="server">
          <a id="aEvents" runat="server" href="Default.aspx?content=CategoryView&CategoryID=19">     
               <asp:Image ID="Image3" runat="server"  ImageUrl="~/Images/WEBSite/f3dbd0ad-697a-4aa6-b253-283e22c6109e_s.jpg"/>      
               <div class="subpagetitle">           
                   <h4><asp:Label ID="Label4" runat="server" Text="<%$Resources:Vikkisoft, Events %>">:</asp:Label></h4>
                </div>
         </a>
      </li>
      </ul>
         </asp:Panel>
        </li>
        <li>
<asp:Panel ID="pnlFooterContentBlock" runat="server" class="FooterContentBlock">   
      <asp:Panel id="pnlCategoryContent" runat="server" class="categoryName">
       <h3><asp:Label ID="lblName" runat="server" Text=""></asp:Label></h3>
       <asp:Label ID="lblCategoryContent" runat="server" Text=""></asp:Label> 
     </asp:Panel>
    <asp:Panel ID="pnlBooking" runat="server" Visible="false" CssClass="Bookingpanel">
        <asp:Label ID="lblPleaseFillInData" runat="server" CssClass="PleaseFillInData" Text="<%$Resources:Vikkisoft, PleaseFillInData %>" />  
             <table border="0"  style="margin-left:30px; margin-right:30px;"  id="tablebookingpage" >

              <tr style="height:20px;">
                  <td><%=CheckInRes%> *</td>
                  <td> <%=RoomsRes%></td>
                  <td><%=AdultsRes%> </td>
                  <td> <%=ChildrenRes%></td>
                   <td>E-mail:*</td>
                  <td><%=Room%></td>
                  <td><%=Message%></td>                 
              </tr>
              <tr  style="height:25px;">
                 <td style=" padding:5px;">  
                      <uc1:DatePicker id="dpCheckInDate1" runat="server" IsRequire="true" Width="140" ValidationGroup="BookingValidation" OnClientDateSelected="VIKKI_CheckInDateSelected1"></uc1:DatePicker> 
                 </td>                        
                 <td><telerik:RadComboBox ID="ddlRooms1" runat="server" Width="50px" EnableLoadOnDemand="false" CssClass="homedropdown">
                      </telerik:RadComboBox> 
                 </td>
                 <td>  <telerik:RadComboBox ID="ddlAdults1" runat="server" Width="50px" EnableLoadOnDemand="false" CssClass="homedropdown">
                       </telerik:RadComboBox>
                 </td>
                 <td> <telerik:RadComboBox ID="ddlChildren1" runat="server" Width="50px" EnableLoadOnDemand="false" CssClass="homedropdown">
                       </telerik:RadComboBox>
                 </td>
                 <td>  
                       <ul style="list-style-type: none; list-style-image: none">
                       <li >
                           <asp:TextBox ID="tbEmail1" runat="server" Width="165px" Height="20px"   CssClass="inputstyle"></asp:TextBox>
                       </li>
                       <li>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbEmail1" Display="Dynamic" ValidationGroup="BookingValidation"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$Resources:Vikkisoft, IncorrectEmail %>" ControlToValidate="tbEmail1" Display="Dynamic" ValidationGroup="BookingValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </li>
                       </ul>
                 </td>
                  <td>
                      <telerik:RadComboBox ID="ddlRoomPrices" runat="server" Width="135px" EnableLoadOnDemand="false" DropDownWidth="230px"></telerik:RadComboBox> 
                  </td>
                  <td rowspan="3" width="220px">
                    <asp:TextBox ID="tbMessage" runat="server" Width="180px" Height="70px" TextMode="MultiLine" CssClass="inputstyle"></asp:TextBox>
                          <div class="VIKKI_HiddenButton">
                        <asp:TextBox ID="tbCFH1" runat="server" Width="165px"  BackColor="White" CssClass="inputstyle"></asp:TextBox>
                        </div>  
                      <asp:Button ID="btnBookNow1" runat="server" OnClick="btnBookNow1_Click" Text="<%$Resources:Vikkisoft, BookNow %>" CssClass="formbutton" ValidationGroup="BookingValidation"/>
                  </td>
              </tr>
              <tr style="height:20px;">
                  <td>
                      <%=CheckOutRes%> *
                  </td>
                  <td colspan="3">
                      <%=Name%>*
                  </td>
                  <td>
                      <%=Phone%>
                  </td> 
              </tr>
              <tr>
                  <td >
                    <uc1:DatePicker id="dpCheckOutDate1" runat="server" IsRequire="true" Width="140" ValidationGroup="BookingValidation"></uc1:DatePicker>  <br />
                    <asp:Label ID="dateCompare1" runat="server" Visible="False" Text="<%$Resources:Vikkisoft, DateCompareError %>"></asp:Label>
                  </td>
                  <td colspan="3"> 
                    <ul style="list-style-type: none; list-style-image: none">
                        <li><asp:TextBox ID="tbName1" runat="server" Width="165px" Height="20px"  CssClass="inputstyle"></asp:TextBox></li>
                        <li> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbName1" Display="Dynamic" ValidationGroup="BookingValidation"></asp:RequiredFieldValidator></li>
                    </ul>
                  </td>
                  <td>
                      <asp:TextBox ID="tbPhone" runat="server" Width="165px" Height="20px"  CssClass="inputstyle"></asp:TextBox>  
                  </td>
                  <td></td>
              </tr>       
                <tr>
                    <td colspan="7">
                      <div style="font-size:14pt; color:#ffc33f; font-weight:bold;">
                        <asp:Label ID="lbError1" runat="server" Text="" Visible="False"></asp:Label>
           </div>   
            </td></tr>
          </table>   
        </asp:Panel>      
    <asp:Panel ID="pnlGallery" runat="server" Visible="false" CssClass="Gallerystyle1">
       <div class="Gallerystyle"> <div id="thumb-tray" class="load-item">
		    <div id="thumb-back"></div>
		    <div id="thumb-forward"></div>
	    </div></div>
        </asp:Panel>              
        <asp:Panel ID="pnlContactUS" runat="server" Visible="false">
        <table cellpadding="0" cellspacing="0"  border="0" style=" float:right; margin:0px 20px 0px 20px;" >
        <tr>
            <td style="width: 110px; text-align:left; height:27px;">
                <asp:Label ID="lblYourName" runat="server" Text="<%$Resources:Vikkisoft, YourName %>"></asp:Label>
            </td>
            <td style="width: 110px; text-align:left; vertical-align:top;">
                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Vikkisoft, Message %>">:</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height:27px;" >
                <asp:TextBox ID="tbName" runat="server" TabIndex="1" Width="220px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbName" Display="Dynamic" ValidationGroup="ContactUsValidation"></asp:RequiredFieldValidator>
            </td>
            <td  ROWSPAN="3">
                <asp:TextBox ID="tdComments"  runat="server" TabIndex="6" Width="300px"  TextMode="MultiLine" Rows="7" MaxLength="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvComment" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tdComments" Display="Dynamic" ValidationGroup="ContactUsValidation"></asp:RequiredFieldValidator>
            </td>
        </tr>                           
        <tr style="height:27px;">
            <td style="width: 110px; text-align:left; height:27px;">
                E-mail:
            </td>
            </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbEmailAddress" runat="server" TabIndex="2" Width="220px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="<%$Resources:Vikkisoft, RequiredField %>" ControlToValidate="tbEmailAddress" Display="Dynamic" ValidationGroup="ContactUsValidation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="<%$Resources:Vikkisoft, IncorrectEmail %>" ControlToValidate="tbEmailAddress" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ContactUsValidation"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="VIKKI_HiddenButton">
            <td>
            </td>
            <td>
                <asp:TextBox ID="tbCFH" runat="server" TabIndex="7" Width="350px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td>
            </td>
            <td style="text-align:right;  padding:10px 15px 0px 3px; " >
                <asp:Button ID="btnSendMessage" runat="server" OnClick="btnSendMessage_Click" Text=" <%$Resources:Vikkisoft, SendMessage %> "  CssClass="formbutton" ValidationGroup="ContactUsValidation" />
            </td>
        </tr>
        <tr>
            <td  colspan="2">
                <div style="font-size:10pt; color:#d60000; font-weight:bold;">
                    <asp:Label ID="lbError" runat="server" Text="" Visible="False"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
        </asp:Panel>                
</asp:Panel>
       <asp:Panel ID="pnlRoomPrice" runat="server" Visible="false">
          <div class="PriceRoomstyle"> 
            <asp:Label ID="lblPriceRoom" runat="server" Text="<%$Resources:Vikkisoft, RoomPrice %>" CssClass="PriceRoomTitle" />
            <table id="tblRoomPrices" runat="server" style="width: 200px;">
            </table>
          </div> 
        </asp:Panel>
        </li>
    </ul>
   
    
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