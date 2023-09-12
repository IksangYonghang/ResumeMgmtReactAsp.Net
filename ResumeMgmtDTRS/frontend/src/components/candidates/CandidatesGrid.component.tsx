import { Box } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { GridColDef } from "@mui/x-data-grid/models";
import { ICandidate } from "../../types/global.typing";
import { baseUrl } from "../../constants/url.constants";
import { PictureAsPdf } from "@mui/icons-material";
import "./candidates-grid.scss";

const column: GridColDef[] = [
  { field: "id", headerName: "ID", width: 100 },
  { field: "firstName", headerName: "First Name", width: 120 },
  { field: "lastName", headerName: "Last Name", width: 150 },
  { field: "email", headerName: "Email", width: 300 },
  { field: "phoneNumber", headerName: "Phone Number", width: 200 },
  { field: "coverLetter", headerName: "CV", width: 500 },
  {
    field: "resumeUrl",
    headerName: "Download",
    width: 150,
    renderCell: (params) => (
      <a
        href={`${baseUrl}/Candidate/download/${params.row.resumeUrl}`}
        download
      >
        <PictureAsPdf />
      </a>
    ),
  },
];

interface ICandidatesGridProps {
  data: ICandidate[];
}

const CandidatesGrid = ({ data }: ICandidatesGridProps) => {
  return (
    <Box sx={{ width: "100%", height: 450 }} className="candidates-grid">
      <DataGrid
        rows={data}
        columns={column}
        getRowId={(row) => row.id}
        rowHeight={50}
      />
    </Box>
  );
};

export default CandidatesGrid;
