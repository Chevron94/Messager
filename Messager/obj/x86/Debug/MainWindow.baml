   M S B A M L   `   `   `  ����C  ?Messager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MessagerMessager  5   	       Messager.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   (localclr-namespace:Messager     5   	   -   5          � Title$  Messager��5   	   $	��412��6   $	��660��6'   $	��660��63    � 	Resources 5        Messager.OnlineConverter   OnlineConverter"  Messager.NewMessageConverter  NewMessageConverter  OnlineStyle �� 
TargetType*  $System.Windows.Controls.DataGridCell �� Property �� Value �� 	Converter  NewMessageStatus  	TextStyle  TextStyleWithoutHorizontal��%~  &	        &	      &	 &     7   &	 �     7  &	 .    &	 �      5   
   6      5   
   6$   �� 5
   
   "  6$   .�5      ��  � �� 6+   �� *Online+8   �� 5       � ��
 �   �6+   5      �� 5   
   "  6)   .�5      ��  � �� 6+   �� *LastMessage.Readed+8   �� 5       � ��
 �   �6+   5      �� 5   
   " ��6"   .�5      ��  � ��$ Center=�64   �� 5       � ��$ Center=�62   �� 5       � ��$
 Wrap=�67   5      �� 5   
   " ��63   .�5      ��  � ��$ Center=�62   �� 5       � ��$
 Wrap=�67   5      5      .��5       �  � ColumnDefinitions 5!   
   �� 5"      $��53 �6   �� 5#      $	��122 �6   �� 5$      $��69 �6   �� 5%      $	��122 �6   �� 5&      $��36 �6   �� 5'      $��85 �6   �� 5(      $	��150 �6    � RowDefinitions 5*   
   �� 5+      $��20 �6   �� 5,      $��25 �6   �� 5-      $��8 �6   �� 5.      $��* �6   .+�50   
   &   System.Windows.Controls.DataGrid  -   5        $��	dataGrid1��50   /      AutoGenerateColumns .  $��3��6@   $��7��6t   ��6M   �� 	 �� Source$	 Friends��
   Columns
 51      4  .System.Windows.Controls.DataGridTemplateColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ Photo��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 68      CellTemplate 54      �� 55      .R�56      �� $��50��6=   $��50��6I   ��6$   �� *Photo+6I   57      59      0  *System.Windows.Controls.DataGridTextColumn  5;      $ NickName��6%   $	 100 6T      	CellStyle# [ 6`   1	  +System.Windows.Controls.DataGridBoundColumn 	  ElementStyle# [ 6�      
IsReadOnly .  	  Binding 67   �� *
NickName+6�     5<      $	 Age��6%   $ 40 6J   # [ 6U    .  62   �� *Age+6     5>      $
 Flag��6)   $ 40 67    5?      �� 5@      .R�5A      �� $��30��6D   $��30��6P   ��6$   �� *Country_Flag+6P   5B      5D        5F      $ Country��6%   $ 90 6R   # [ 6]    .  66   �� *	Country+6�     5G      $
 City��6%   $ 80 6L   # [ 6W    .  63   �� *City+6�     5H      $ 
LastMesage��6%   $	 150 69   # [ 6e   # [ 6�    .  6E   �� *LastMessage+6�     5I      $ Messages��6)    5J      �� 5K      .R�5L      �� $��MessageHistory��6%   -   5        $��Messages��5L   ;   6m   5M      5O      5Q      �� 5R   
   -   5        $��Login��5R   /   $��1��6   $��1��6    �� MaxLines$ 1��6<   $��0��6I   �� 5S   
   -   5        $��
AuthButton��5S   !   $��Enter��6   $��5��63   $��1��6C   $��2��6P   6]   G� 5T   
   -   5        $��Password��5T   3   $��1��6   $��3��6#   $��25��6D   $��1��6P   �� 5U   
   $��Login��6   $��1��6    $��0��6-   $��4q�6=   �� 5V   
   $��Password��6   $��2��6#   $��1��63   $��4q�6@   5W      5X      