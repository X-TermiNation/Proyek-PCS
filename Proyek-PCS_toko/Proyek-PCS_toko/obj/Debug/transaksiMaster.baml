   M S B A M L   `   `   `  ����J  FProyek-PCS_toko, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Proyek_PCS_tokoProyek_PCS_toko  5   	   %    Proyek_PCS_toko.transaksiMaster   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Proyek_PCS_toko     5   	     � Title$  transaksiMaster��5   	   $��549.5��6!   $��551.5��60   .��5	      � .+�5
   
   �� -   5         ��Name$ btnback��5
      $��Back to menu��6"   $
��Left=�69   $��430,479,0,0q�6T   $	��Top=�6i   $	��104��6�   $��30��6�   6�   � 5   
   -   5         �Name$ gridmastertransaksi��5      $
��Left=�6,   $	��413��6G   $��	10,34,0,0q�6T   $	��Top=�6g   $	��524��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvTransaksi��5      $
��Left=�6-   $	��380��6H   $	��Top=�6U   $	��514��6m      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6y      
IsReadOnly . 6�   5      � 5   
   -   5        $ 	gridsaldo��5      $
��Left=�6"   $	��429��6=   $��0,18,0,0q�6J   $	��Top=�6\   $	��534��6t   .+�5        -   5        $ dgvsaldo��5      $
��Left=�6)   $	��376��6D   $��16,0,0,0q�6Q   $	��Top=�6c   $	��514��6{   $ * 6�    . 6�   �� 5      -   5         ��Name$ 
tbgetsaldo��5      $
��Left=�6*   $��24��6E   $��
31,399,0,0q�6Q    �� TextWrapping$
 Wrap=�6e   $	��Top=�6y   $	��230��6�    �� 
IsReadOnly . 6�   �� 5      -   5        $ 	btnreject��5      $��Reject��6(   $
��Left=�69   $��415,399,0,0q�6T   $	��Top=�6i   $	��102��6�   $��24��6�   6�   �� 5      -   5        $ 	btnaccept��5      $��Accept��6(   $
��Left=�69   $��293,399,0,0q�6T   $	��Top=�6i   $	��102��6�   $��24��6�   6�   5      �� 5   
   -	   5        $ 	btnchange��5      $��Goto Request Saldo��6$   $
��Left=�6A   $��
10,479,0,0q�6\   $	��Top=�6p   $	��132��6�   $��30��6�   6�   5      5      