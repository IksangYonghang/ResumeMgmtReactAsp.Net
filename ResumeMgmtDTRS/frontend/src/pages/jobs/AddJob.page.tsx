import { useState, useEffect } from "react";
import "./jobs.scss";
import {
  ICompany,
  ICreateCompanyDto,
  ICreateJobDto,
} from "../../types/global.typing";
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

const levelsArray: string[] = [
  "Intern",
  "Jonior",
  "MidLever",
  "Senior",
  "TeamLead",
  "Cto",
  "Architect",
];

const AddJob = () => {
  const [job, setJOb] = useState<ICreateJobDto>({
    title: "",
    level: "",
    companyId: "",
  });
  const [companies, setCompanies] = useState<ICompany[]>([]);
  const redirect = useNavigate();

  //Populating company list from Company Page

  useEffect(() => {
    httpModule
      .get<ICompany[]>("/Company/Get")
      .then((response) => {
        setCompanies(response.data);
      })
      .catch((error) => {
        alert("Error");
        console.log(error);
      });
  }, []);

  const handleClickSaveBtn = () => {
    if (job.title === "" || job.level === "" || job.companyId === "") {
      alert("Field is required");
      return;
    }

    httpModule
      .post("/Job/Create", job)
      .then((response) => redirect("/jobs"))
      .catch((error) => console.log(error));
  };

  const handleClickBackBtn = () => {
    redirect("/jobs");
  };

  return (
    <div className="content">
      <div className="add-job">
        <h2>Add a new job</h2>
        <TextField
          fullWidth
          autoComplete="off"
          label="Job Name"
          variant="outlined"
          value={job.title}
          onChange={(n) => setJOb({ ...job, title: n.target.value })}
        />
        <FormControl fullWidth>
          <InputLabel>Job Type</InputLabel>
          <Select
            label="Job Type"
            value={job.level}
            onChange={(s) => setJOb({ ...job, level: s.target.value })}
          >
            {levelsArray.map((item) => (
              <MenuItem key={item} value={item}>
                {item}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControl fullWidth>
          <InputLabel>Company</InputLabel>
          <Select
            label="Company Name"
            value={job.companyId}
            onChange={(s) => setJOb({ ...job, companyId: s.target.value })}
          >
            {companies.map((item) => (
              <MenuItem key={item.id} value={item.id}>
                {item.name}
              </MenuItem>
            ))}
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

export default AddJob;
