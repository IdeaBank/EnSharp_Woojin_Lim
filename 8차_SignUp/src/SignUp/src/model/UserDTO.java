package model;

import com.mysql.cj.protocol.a.SqlDateValueEncoder;

import java.util.Date;
import java.text.ParseException;
import java.text.SimpleDateFormat;

public class UserDTO {
    private String id;
    private String name;
    private String password;
    private Date birthdate;
    private String email;
    private String phoneNumber;
    private String address;
    private int addressNumber;

    public UserDTO() {

    }

    public UserDTO(String id, String name, String password, Date birthdate, String email, String phoneNumber, String address, int addressNumber) {
        this.id = id;
        this.name = name;
        this.password = password;
        this.birthdate = birthdate;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.address = address;
        this.addressNumber = addressNumber;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public Date getBirthdate() {
        return birthdate;
    }

    public void setBirthdate(String birthdate) {
        try {
            this.birthdate = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss").parse(birthdate);
        }
        catch(ParseException e) {
            try {
                this.birthdate = new SimpleDateFormat("yyMMdd").parse(birthdate);
            }
            catch(ParseException exc) {
                this.birthdate = new Date();
            }
        }
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public int getAddressNumber() {
        return addressNumber;
    }

    public void setAddressNumber(int addressNumber) {
        this.addressNumber = addressNumber;
    }
}
