   M S B A M L   `   `   `  ����K  GGroupProject3280, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:GroupProject3280.MainGroupProject3280.Main  5   	   #    GroupProject3280.Main.wndMain   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:GroupProject3280.Main     5   	   -   5          � Title$  wndMain��5   	   $	��400��6   $	��600��6&   .��5	      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   $��0��6!   .+�5      o� $	��Top=�6   .@�5      m� $��Invoice��6   .@�5      m� -   5         m�Name$ miSearch��5      $��Search��61   6A   �� 5      m� 5      -   5        $ miNew��5      $	��New��6.   6;   m� 5      -   5        $ miEdit��5      $
��Edit��6/   {�.  6_   m� 5      -   5        $ miDelete��5      $��Delete��61   {�.  6d   5      m� 5      $��Items��6   .@�5      m� -   5        $ miUpdate��5      $��Update��61   6A   5      5      5      �� 5    
   $��1��6   $��0��6   .+�5!      �� -   5         ��Name$ lblInvoiceNumber��5!      $��Invoice # :��6.   $
��Left=�6D   $	��Top=�6_    �� Left$ 10��6w   
 �� Top$ 10��6�   �� 5"      $��Invoice Date :��6   $
��Left=�6-   $	��Top=�6H    J� RenderTransformOrigin$ 
0.88,4.544&�6`   $	 108��6�   $ 10��6�   (  "System.Windows.Controls.DatePicker  5#      -   5          Name$ dpInvoiceDate��5#      $
��Left=�60   $	��Top=�6K   $	 192��6c   $ 11��6u   {�.  6�   �� 5$      -	   5        	 ��Name$	 cboItems��5$      $	��155��6)   $	 415��65   $ 41��6G   {�.  6�   &   System.Windows.Controls.DataGrid  5%      -
   5        
  Name$
 dgInvoiceItems��5%   3      AutoGenerateColumns .  $	��290��6K   $	��400��6X   $ 10��6d   $ 41��6u   {�.     Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Code��6)      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   ��  �� Path$ ItemCode�6^     5(      $ Item Description��6)    .  6C   �� $
 Desc�6a     5)      $
 Cost��6)    .  67   �� $
 Cost�6U   4  .System.Windows.Controls.DataGridCheckBoxColumn  5*      $ Select��6-    6=   �� $ Selected�5,      �� 5-      -   5         ��Name$ btnAdd��5-      $��	Add Items��6%   $��75��69   $	 415��6D   $ 99��6V   {�.  6{   �� 5.      -   5        $ 	btnRemove��5.      $��Remove Selected Items��6(   $
��Left=�6H   $	��Top=�6c   $	��150��6{   $	 415��6�   $	 124��6�   {�.  6�   �� 5/      -   5        $ lblTotal��5/      $��Total: ��6&   $	 415��68   $	 280��6J   $ 
6.642,0.23&�6[   �� 50      -   5        $ 	btnCancel��50      $
��Left=�6(   $	��Top=�6C   $	 480��6[   $	 311��6m   $��Cancel��6~   $��60��6�   {�.  6�   �� 51      -   5        $ btnSave��51      $
��Save��6&   $
��Left=�65   $	��Top=�6P   $	 415��6h   $	 311��6z   $��60��6�   {�.  6�   �� 52      -   5        $ lblCost��52      $��Cost:��6%   $
��Left=�65   $	��Top=�6P   $	 415��6h   $ 68��6z   �� 53      -   5        $ lblInvoiceDateError��53      $��Select an Invoice Date��61   $
��Left=�6R   $	��Top=�6m   $	 299��6�   $ 11��6�   
���  ��{�.  $y�Hidden=�6�   54      55      56      