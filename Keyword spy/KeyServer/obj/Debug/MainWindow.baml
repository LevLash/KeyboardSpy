   M S B A M L   `   `   `  ����D  @KeyServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:KeyServer	KeyServer  5   	       KeyServer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:KeyServer     5   	     � Title$  Server��5   	   $	��450��6   $	��800��6%   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��271* �6   �� 5      $
��125* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   o� $��0��6   $��0��6   $��2��6,   .@�5      m� $
��Menu��6   .@�5      m� -   5         m�Name$ 	btn_Start��5      $��Start server��6.   6h   m� 5      -   5        $��Clear��5      6H   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgr_Info��5      $��1��6%   $��0��62      AutoGenerateColumns . 6B     5   
   -   5        $ 
dgr_Status��5      $��1��6'   $��1��64    . 6D   5      5      