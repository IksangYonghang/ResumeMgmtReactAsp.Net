import { useState } from "react";
import "./companies.scss";
import { ICreateCompanyDto } from "../../types/global.typing";
import {
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";
import { error } from "console";

const AddCompany = () => {
  const [company, setCompany] = useState<ICreateCompanyDto>({
    name: "",
    size: "",
  });
  const redirect = useNavigate();

  const handleClickSaveBtn = () => {
    if (company.name === " " || company.size === "") {
      alert("Field is required");
      return;
    }
    httpModule
      .post("/Company/Create", company)
      .then((response) => redirect("/companies"))
      .catch((error) => console.log(error));
  };
  const handleClickBackBtn = () => {
    redirect("/companies");
  };

  return (
    <div className="content">
      <div className="add-company">
        <h2>Add a new conpany</h2>
        <TextField
          fullWidth
          autoComplete="off"
          label="Company Name"
          variant="outlined"
          value={company.name}
          onChange={(n) => setCompany({ ...company, name: n.target.value })}
        />
        <FormControl fullWidth>
          <InputLabel>Company Size</InputLabel>
          <Select
            label="Company Size"
            value={company.size}
            onChange={(s) => setCompany({ ...company, size: s.target.value })}
          >
            <MenuItem value="Small">Small</MenuItem>
            <MenuItem value="Medium">Medium</MenuItem>
            <MenuItem value="Large">Large</MenuItem>
          </Select>
        </FormControl>
        <div className="btns">
          <Button
            variant="outlined"
            color="primary"
            onClick={handleClickSaveBtn}
          >
            Save
          </Button>
          <Button
            variant="outlined"
            color="secondary"
            onClick={handleClickBackBtn}
          >
            Back
          </Button>
        </div>
      </div>
    </div>
  );
};

export default AddCompany;
