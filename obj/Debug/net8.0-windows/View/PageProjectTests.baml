   M S B A M L   `   `   `  ����B  >Trackio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Trackio.ViewTrackio.View  5      #    Trackio.View.PageProjectTests   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Trackio.View     5        N� Title$  PageProjectProperties��5	      .��5      � ���	#FFD0D0D0.+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgProjectTests��5         AutoGenerateColumns$ False��6)   $��0,0,0,99q�6E      CanUserAddRows$ False��6W      SelectionMode$ Single=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        
dgtcID��,  &System.Windows.Controls.DataGridColumn   Header$ ID��5   5      
IsReadOnly$
 True��6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *iID+6Y     5      -   5        dgtcTestName��$ Test's Name��5   <      Width5  /System.Windows.Controls.DataGridLengthConverter$	 400 6q    6Q   �� *sNameOfTest+6q     5      -   5        dgtcRunsCount��$ Runs' Count��5   >   $
 True��6t    6S   �� *iRunsCounter+6t   4  .System.Windows.Controls.DataGridComboBoxColumn  5      -   5        sCurrentStatus��$ Current Status��5   A   	   SelectedValueBinding	 6Y   �� *sCurrentStatus+
 �� Mode$
 TwoWay=�  5      -   5        dgtcComment��$ Comment��5   ;   $ * 6i    6L   �� *
sComment+5      5      �� 5   
   -   5        $��Add New Test��5      $
��Left=�6(   $��
10,356,0,0q�6C   $	��Top=�6W   $	��128��6�   �� 5   
   -   5        $��Remove Selected Test��5      $
��Left=�60   $��143,356,0,0q�6K   $	��Top=�6`   $	��124��6�   �� 5   
   -	   5        $
��Save��5      $
��Left=�6    $��271,356,0,0q�6;   $	��Top=�6P   $	��115��6u   �� 5   
   -
   5        $��Run All Tests��5      $
��Left=�6)   $��598,388,0,0q�6D   $	��Top=�6Y   $	��102��6q   6}   �� 5   
   -   5        $��Close��5      $
��Left=�6!   $��720,388,0,0q�6<   $	��Top=�6Q   $��66��6i   6t   5      5      