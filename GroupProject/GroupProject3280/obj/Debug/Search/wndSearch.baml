   M S B A M L   `   `   `  ����K  GGroupProject3280, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GroupProject3280.SearchGroupProject3280.Search  5   	   '    !GroupProject3280.Search.wndSearch   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:GroupProject3280.Search     5   	   -   5          � Title$  Search��5   	   $	��485��6   $	��800��6%    � 
ResizeMode$ NoResize=�61   .��5	      � $	��430��6   .+�5
   
   �� -   5         ��Name$ 	mainLabel��5
      $��Invoice Table��6#   $
��Left=�6;   $��30��6V   $��
349,10,0,0q�6b   $	��Top=�6v   $��83��6�   $
��Bold"�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	DataTable��5      $
��Left=�6&   $	��270��6A   $��
10,114,0,0q�6N   $	��Top=�6b   $	��772��6z      CanUserAddRows .  6�   �� 5   
   -   5         ��Name$ invoiceIDSelector��5      $
��Left=�6.   $��
330,62,0,0q�6I   $	��Top=�6]   $	��120��6u   6�   �� 5   
   -   5        $ chargeAmountSelector��5      $
��Left=�61   $��
455,62,0,0q�6L   $	��Top=�6`   $	��120��6x   6�   �� 5   
   -   5        $ dateSelector��5      $
��Left=�6)   $��
205,62,0,0q�6D   $	��Top=�6X   $	��120��6p    J� RenderTransformOrigin$ 0.489,1.515&�6|   6�   �� 5   
   -   5        $ 	dateLabel��5      $
��Date��6#   $
��Left=�62   $��26��6M   $��
246,36,0,0q�6Y   $	��Top=�6m   $��36��6�   �� 5   
   -   5        $ invoiceLabel��5      $��	Invoice #��6&   $
��Left=�6:   $��26��6U   $��
360,36,0,0q�6a   $	��Top=�6u   $��60��6�   �� 5   
   -	   5        $ chargeLabel��5      $��Charge Amount��6%   $
��Left=�6=   $��26��6X   $��
466,36,0,0q�6d   $	��Top=�6x   $	��100��6�   �� 5   
   -
   5        $��Select Invoice��5      $
��Left=�6*   $��611,391,0,0q�6E   $	��Top=�6Z   $��83��6r   6}   �� 5   
   -   5         ��Name$ Cancel��5      $��Cancel��6!   $
��Left=�62   $��699,391,0,0q�6M   $	��Top=�6b   $��83��6z   6�   �� 5   
   -   5        $ clearSelections��5      $��Clear��6*   $
��Left=�6:   $��
349,89,0,0q�6U   $	��Top=�6i   $��83��6�   6�   5      5      