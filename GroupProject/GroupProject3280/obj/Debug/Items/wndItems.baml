   M S B A M L   `   `   `  ����K  GGroupProject3280, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:GroupProject3280.ItemsGroupProject3280.Items  5   	   3clr-namespace:GroupProject3280GroupProject3280  5   	   %    GroupProject3280.Items.wndItems   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:GroupProject3280.Items     5   	   ;GroupProject3280clr-namespace:GroupProject3280     5   	   -   5          �Name$  wndItemsWindow��5   A    � Title$ Update Items��5	   	   $��12(�6   $	��525��6K   $	��700��6X    � 	Resources 5
      %   GroupProject3280.InvoiceDataSet   invoiceDataSet  itemDescViewSource �� Source �� Source��%Y   &	        &	      7     5   
   6*   �� 5   
    6:   �� *
ItemDesc+8   5      .��5      �  � DataContext# [ 6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ itemDescDataGrid��5         AutoGenerateColumns .     EnableRowVirtualization . 	   
IsReadOnly	 . $��10,10,347.333,15q�6�   
   RowDetailsVisibilityMode$
 VisibleWhenSelected=�6�      CanUserAddRows .  ��6h   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        itemCodeColumn��,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Code��5   Z      Width5  /System.Windows.Controls.DataGridLengthConverter$ 75 6m   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *
ItemCode+6m     5      -   5        itemDescColumn��$ 	Item Desc��5   Z   $	 150 6m    6=   �� *
ItemDesc+6m     5      -   5        
costColumn��$
 Cost��5   R   $ 75 6`    69   �� *Cost+6`   5      �� 5   
   -   5         ��Name$ 	gbAddItem��5      $��Add Item��6&   $
��Left=�68   $	��172��6S   $��
362,14,0,0q�6`   $	��Top=�6t   $	��316��6�   .��5      �� -   5         ��Name$ 	spAddItem��5      $��0,0,1,0.333q�6,    �� Orientation$ Vertical=�6A   $��Center=�6X   $	��303��6u   .+�5      �� -   5        $ spAddItemL1��5      $��0,0,0.333,0q�62   $ 
Horizontal=�6G   $��30��6`   .+�5      �� -	   5         ��Name$ lAddItemNum��5      $��	Item Code��61   $
��Left=�6E   $��49,0,0,-0.333q�6`   $	��Top=�6w   �� 5      -
   5         ��Name$ tbAddItemCode��5      $
��Left=�65   $��29��6P   $��Center=�6\   $��Center=�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��50��6�    �� 	MaxLength$ 4��6�   6�   5      �� 5      -   5        $ spAddItemLine2��5      .+�5      �� -   5        $ lAddItemCodeErr��5      $��Invalid Item Code��65   $
��Left=�6Q   $��
114,-5,0,0q�6l   $	��Top=�6�   $��12(�6�   
���  ��6�   5       �� 5!      -   5        $ spAddItemL3��5!      $��0,0,0.333,0q�62   $ 
Horizontal=�6G   $��30��6`   $��Right=�6l   .+�5"      �� -   5        $ lAddItemDesc��5"      $��Item Description��62   $
��Left=�6M   $��0,0,0,-0.333q�6h   $	��Top=�6~   �� 5#      -   5        $ tbAddItemDesc��5#      $
��Left=�65   $��Center=�6P   $��29��6r   $��0,0,0,-0.333q�6~   $
 Wrap=�6�   $	��Top=�6�   $	��190��6�   $ 50��6�   6�   5%      �� 5&      -   5        $ spAddItemLine4��5&      .+�5'      �� -   5        $ lAddItemDescErr��5'      $��Invalid Item Description��65   $
��Left=�6X   $��
114,-5,0,0q�6s   $	��Top=�6�   $��12(�6�   
���  ��6�   5(      �� 5)      -   5        $ spAddItemL5��5)      $��0,0,0.333,0q�62   $ 
Horizontal=�6G   $��30��6`   .+�5*      �� -   5        $ lAddItemCost��5*      $��Item Cost  $��62   $
��Left=�6I   $��40,0,0,-0.333q�6d   $	��Top=�6{   �� 5+      -   5        $ tbAddItemCost��5+      $
��Left=�65   $��Center=�6P   $��29��6r   $
 Wrap=�6~   $	��Top=�6�   $��50��6�   $ 8��6�   6�   �� 5,      -   5         ��Name$ bAddItem��5,      $	��Add��6/   $��43��6=   $��96,0,0,0q�6H   6Z   5.      �� 5/      -   5        $ spAddItemLine6��5/      .+�50      �� -   5        $ lAddItemCostErr��50      $��Invalid Item Cost��65   $
��Left=�6Q   $��
114,-5,0,0q�6l   $	��Top=�6�   $��12(�6�   
���  ��6�   51      53      54      �� 55   
   -   5        $ 
gbEditItem��55      $��	Edit Item��6'   $	��201��6:   $��362,191,0,0q�6G   $	��Top=�6\   $
��Left=�6t   $	��316��6�   .��56      �� -   5        $ 
spEditItem��56      $��5,1,0,0q�6-   $ Vertical=�6>   $��Center=�6U   $	��306��6r   .+�57      �� -   5        $ spEditItemL1��57      $��0,0,0.333,10q�63   $ 
Horizontal=�6I   $��30��6b   .+�58      �� $��Select Item:��6   $��25,0,0,0q�63   �� 59      -   5         ��Name$ 
cbEditItem��59      $	��190��63   $��29��6?   $��5,0,0,0q�6K   6\   5<      �� 5=      -   5        $ spEditItemL2��5=      $��0,0,0.333,10q�63   $ 
Horizontal=�6I   $��30��6b   .+�5>      �� -   5        $ lEditItemNum��5>      $��	Item Code��62   $
��Left=�6F   $��38,0,0,-0.333q�6a   $	��Top=�6x   �� 5?      -   5        $ tbEditItemCode��5?      $
��Left=�66   $��29��6Q   $��Center=�6]   $��Center=�6�   $
 Wrap=�6�   $	��Top=�6�   $��50��6�   {�.  $��0q�6�   $ 4��6�   6  5B      �� 5C      -   5        $ spEditItemL3��5C      $��0,0,0.333,0q�63   $ 
Horizontal=�6H   $��30��6a   .+�5D      �� -    5        $ lEditItemName��5D      $��Item Description��63   $
��Left=�6N   $��5,0,0,-0.333q�6i   $	��Top=�6   �� 5E      -!   5        $ tbEditItemDesc��5E      $��Center=�66   $��29��6X   $��0,0,0,-0.333q�6d   $
 Wrap=�6z   $	��Top=�6�   $	��190��6�   $
��Left=�6�   {�.  $ 50��6�   6�   5G      �� 5H      -"   5        $ spEditItemLine4��5H      .+�5I      �� -#   5        $ lEditItemDescErr��5I      $��Invalid Item Description��66   $
��Left=�6Y   $��
113,-5,0,0q�6t   $	��Top=�6�   $��12(�6�   
���  ��6�   5J      �� 5K      -$   5        $ spEditItemL5��5K      $��0,0,0.333,0q�63   $ 
Horizontal=�6H   $��30��6a   .+�5L      �� -%   5        $ lEditItemCost��5L      $��Item Cost  $��63   $
��Left=�6J   $��28,0,0,-0.333q�6e   $	��Top=�6|   �� 5M      -&   5        $ tbEditItemCost��5M      $
��Left=�66   $��Center=�6Q   $��29��6s   $
 Wrap=�6   $	��Top=�6�   $��50��6�   {�.  $ 8��6�   6�   �� 5N      -'   5        $ bEditItemSave��5N      $
��Save��64   $��43��6C   $��95,0,0,0q�6N   {�.  6r   5P      �� 5Q      -(   5        $ spEditItemLine6��5Q      .+�5R      �� -)   5        $ lEditItemCostErr��5R      $��Invalid Item Cost��66   $
��Left=�6R   $��
114,-5,0,0q�6m   $	��Top=�6�   $��12(�6�   
���  ��6�   5S      5U      5V      �� 5W   
   -*   5        $ gbDeleteItem��5W      $��Delete Item��6)   $
��Left=�6>   $��84��6Y   $��362,397,0,0q�6e   $	��Top=�6z   $	��316��6�   .��5X      �� -+   5        $ spDeleteItem��5X      $��5,15,1,0q�6/   $ 
Horizontal=�6A   $	��298��6Z   $��38��6f   $	��Top=�6r   .+�5Y      �� -,   5        $ cbDeleteItem��5Y      $
��Left=�61   $	��Top=�6L   $	��190��6d   $��29��6p   6|   �� 5\      --   5        $ bDeleteItem��5\      $��Delete��6.   $
��Left=�6?   $	��Top=�6Z   $��75��6r   $��29��6}   $��20,0,0,0q�6�   {�.  6�   5]      5^      5a      5b      